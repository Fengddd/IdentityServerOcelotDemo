using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCore.Common.Event
{
    /// <summary>
    /// 事件和事件处理注册
    /// </summary>
    public interface IEventHandlerRegister
    {
        /// <summary>
        /// 事件注册
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <typeparam name="TEventHandler"></typeparam>
        /// <returns></returns>
        void Register<TEvent,TEventHandler>() where TEvent:IEvent where TEventHandler:IEventHandler;

        /// <summary>
        /// 事件是否注册
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <typeparam name="TEventHandler"></typeparam>
        /// <returns></returns>
        bool IsRegister<TEvent, TEventHandler>() where TEvent : IEvent where TEventHandler:IEventHandler;

        /// <summary>
        /// 事件处理
        /// </summary>
        /// <returns></returns>
        Task HandlerAsync<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}
