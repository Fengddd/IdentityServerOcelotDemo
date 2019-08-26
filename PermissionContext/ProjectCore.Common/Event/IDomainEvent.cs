using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCore.Common.Event
{
    /// <summary>
    /// 领域事件
    /// </summary>
    public interface IDomainEvent:IEvent
    {
        /// <summary>
        /// 聚合根Id
        /// </summary>
        object AggregateRootId { get; set; }
        /// <summary>
        /// 聚合根类型
        /// </summary>
        string AggregateRootType { get; set; }

        /// <summary>
        /// 序列号，版本号
        /// </summary>
        long Version { get; set; }
    }
}
