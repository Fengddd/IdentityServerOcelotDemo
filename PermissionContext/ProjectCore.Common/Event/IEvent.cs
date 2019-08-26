using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCore.Common.Event
{
    /// <summary>
    /// 事件基类
    /// </summary>
    public interface IEvent
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        Guid Id { set; get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreateDateTime { set; get; }
    
    }
}
