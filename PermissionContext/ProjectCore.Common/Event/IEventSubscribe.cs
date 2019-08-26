using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCore.Common.Event
{
    /// <summary>
    /// 订阅事件
    /// </summary>
    public interface IEventSubscribe
    {
        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <returns></returns>
        void Subscribe<TEvent, TEventHandler>() where TEvent : IEvent where TEventHandler:IEventHandler;
    }
}
