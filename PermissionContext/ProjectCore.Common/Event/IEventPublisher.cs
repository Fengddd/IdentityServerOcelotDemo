using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCore.Common.Event
{
    /// <summary>
    /// 发布事件
    /// </summary>
    public interface IEventPublisher
    {
        /// <summary>
        /// 发布事件
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="event"></param>
        /// <returns></returns>
        void Publisher<TEvent>(TEvent @event) where TEvent:IEvent;
    }
}
