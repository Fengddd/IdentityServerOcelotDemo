using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCore.Common.Event.CQRS
{
    /// <summary>
    /// 事件储存
    /// </summary>
    public interface IEventStorage
    {
        /// <summary>
        /// 事件储存保存
        /// </summary>
        Task EventStorageSava(IEvent @event);
    }
}
