using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Polly;

namespace ProjectCore.Common.RedisHelper
{
    /// <summary>
    ///  单例模式获取redis连接实例
    /// </summary>
    public class RedisManager
    {
        private RedisManager()
        {

        }

        private static ConnectionMultiplexer _instance;
        private static readonly object Locker = new object();
        //读取appsettings.json的数据库连接字符串
        static readonly ConnectionService Connection = JsonConfigurationHelper.GetAppSettings<ConnectionService>("appsettings.json", "ConnectionService");
        /// <summary>
        /// 单例模式获取redis连接实例
        /// </summary>      
        public static ConnectionMultiplexer Instance()
        {
         
            lock (Locker)
            {
                if (_instance == null)
                {
                    //_instance = ConnectionMultiplexer.Connect(Connection.ConnectionRedis);
                    _instance = TryRedisConnection();
                }
            }
            return _instance;
        }

        /// <summary>
        /// redis连接异常重试
        /// </summary>
        public static ConnectionMultiplexer TryRedisConnection()
        {
            var policy = Policy.Handle<SocketException>().Or<RedisConnectionException>().Or<InvalidOperationException>()
                .WaitAndRetry(5, p => TimeSpan.FromSeconds(5), (ex, time) =>
                {
                    Debug.Write("测试错误");
                    //记录错误日志
                });
            policy.Execute(() =>
            {
                _instance = ConnectionMultiplexer.Connect(Connection.ConnectionRedis);
            });
            return _instance;
        }

    }
}
