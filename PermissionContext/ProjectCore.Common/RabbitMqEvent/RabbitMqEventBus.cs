using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using ProjectCore.Common.Event;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace ProjectCore.Common.RabbitMqEvent
{
    /// <summary>
    /// RabbitMQ 事件总线
    /// </summary>
    public class RabbitMqEventBus : EventBus
    {
        private readonly IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        private string _exchangeName;
        private readonly string _exchangeType;
        private string _queueName;
        private readonly bool _autoAck;

        public RabbitMqEventBus(IEventHandlerRegister register, IConnectionFactory connectionFactory, string exchangeType, bool autoAck) : base(register)
        {
            _connectionFactory = connectionFactory;
            _connection = connectionFactory.CreateConnection();
            _exchangeType = exchangeType;
            _autoAck = autoAck;            
        }
        /// <summary>
        /// 发布事件
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="event"></param>
        /// <returns></returns>
        public override void Publisher<TEvent>(TEvent @event)
        {
            IsConnTryConn();
            using (var channel = this._connection.CreateModel())
            {
                _exchangeName = GetRabbitMqExchangeName<TEvent>(@event);
                _queueName = GetRabbitMqQueueName<TEvent>(@event);
                channel.ExchangeDeclare(_exchangeName, _exchangeType, durable: false, autoDelete: false, arguments: null);
                //声明一个队列，设置队列是否持久化，排他性，与自动删除
                channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false,
                    arguments: null);
                //绑定消息队列，交换器，
                channel.QueueBind(_queueName, _exchangeName, routingKey: _exchangeName, arguments: null);
                //队列持久化
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                var message = JsonConvert.SerializeObject(@event, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(_exchangeName, _exchangeName, properties, body);
            }
        }

        /// <summary>
        /// 获取RabbitMQ交换机的名称
        /// </summary>
        /// <returns></returns>
        public static string GetRabbitMqExchangeName<TEvent>(TEvent @event)
        {
            return @event.GetType().FullName;
        }

        /// <summary>
        /// 获取RabbitMQ队列的名称
        /// </summary>
        /// <returns></returns>
        public static string GetRabbitMqQueueName<TEvent>(TEvent @event)
        {
            return @event.GetType().FullName;
        }

        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <returns></returns>
        public override void Subscribe<TEvent, TEventHandler>()
        {
            CreateCustomerChannel<TEvent>();
            if (!this._eventHandlerRegister.IsRegister<TEvent, TEventHandler>())
            {
                this._eventHandlerRegister.Register<TEvent, TEventHandler>();
            }
        }

        /// <summary>
        /// 创建通道
        /// </summary>
        /// <returns></returns>
        private IModel CreateCustomerChannel<TEvent>()
        {
            IsConnTryConn();
            //创建通道
            var channel = this._connection.CreateModel();
            //消费者
            var customer = new EventingBasicConsumer(channel);
            customer.Received += async (model, ea) =>
            {
                var eventBody = ea.Body;
                var jsonStr = Encoding.UTF8.GetString(eventBody);
                var @event = (IEvent)JsonConvert.DeserializeObject(jsonStr, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                await this._eventHandlerRegister.HandlerAsync(@event);
                channel.BasicAck(ea.DeliveryTag, multiple: false);
            };
            channel.BasicConsume(queue: typeof(TEvent).FullName, autoAck: false, consumer: customer);

            channel.CallbackException += (sender, ea) =>
            {
                this._channel.Dispose();
                this._channel = CreateCustomerChannel<TEvent>();
            };
            return channel;
        }

        /// <summary>
        /// RabbitMQ 是否连接
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            return this._connection != null && this._connection.IsOpen;
        }

        /// <summary>
        /// 重试连接
        /// </summary>
        /// <returns></returns>
        public bool TryConnect()
        {
            var policy = Policy.Handle<SocketException>().Or<BrokerUnreachableException>()
                .WaitAndRetry(5, p => TimeSpan.FromSeconds(1), (ex, time) =>
                {
                    //记录错误日志
                });
            policy.Execute(() =>
            {
                this._connection = this._connectionFactory.CreateConnection();
            });
            if (IsConnected())
            {
                this._connection.ConnectionShutdown += Connection_ConnectionShutdown;
                this._connection.CallbackException += Connection_CallbackException;
                this._connection.ConnectionBlocked += Connection_ConnectionBlocked;

                return true;
            }
            return false;
        }

        /// <summary>
        /// 连接被阻止的异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Connection_ConnectionBlocked(object sender, RabbitMQ.Client.Events.ConnectionBlockedEventArgs e)
        {
            IsConnTryConn();
        }

        /// <summary>
        /// 回调异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Connection_CallbackException(object sender, RabbitMQ.Client.Events.CallbackExceptionEventArgs e)
        {
            IsConnTryConn();
        }

        /// <summary>
        ///  连接异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Connection_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            IsConnTryConn();
        }

        /// <summary>
        /// 如果连接异常就重试连接 1秒重试1次最多重试5次
        /// </summary>
        public void IsConnTryConn()
        {
            if (!IsConnected())
            {
                TryConnect();
            }
        }

    }
}
