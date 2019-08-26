using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCore.Common.Event
{
    /// <summary>
    /// 时间总线
    /// </summary>
    public abstract class EventBus : IEventBus
    {
        protected readonly IEventHandlerRegister _eventHandlerRegister;

        protected EventBus(IEventHandlerRegister eventHandlerRegister)
        {
            _eventHandlerRegister = eventHandlerRegister;
        }       
        /// <summary>
        /// 发布事件
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="event"></param>
        /// <returns></returns>
        public abstract void Publisher<TEvent>(TEvent @event) where TEvent : IEvent;

        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <returns></returns>
        public abstract void Subscribe<TEvent, TEventHandler>() where TEvent : IEvent where TEventHandler:IEventHandler;

    }
}
