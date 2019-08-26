using System;
using System.Collections.Generic;
using System.Text;
using ProjectCore.Common.Event;
using RabbitMQ.Client;

namespace ProjectCore.Common.RabbitMqEvent
{
    /// <summary>
    /// 应用服务事件总线
    /// </summary>
    public class ApplicationEventBus : RabbitMqEventBus,IApplicationEventBus
    {
        public ApplicationEventBus(IEventHandlerRegister register, IConnectionFactory connectionFactory, string exchangeType, bool autoAck) 
            : base(register, connectionFactory, exchangeType, autoAck)
        {

        }
    }
}
