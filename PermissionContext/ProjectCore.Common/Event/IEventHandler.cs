using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCore.Common.Event
{
    /// <summary>
    /// 事件处理
    /// </summary>
    public interface IEventHandler
    {
        /// <summary>
        /// 事件处理
        /// </summary>
        /// <returns></returns>
        Task<bool> HandlerAsync<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}
