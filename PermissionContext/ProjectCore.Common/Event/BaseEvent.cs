using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCore.Common.Event
{
    /// <summary>
    /// 事件基类
    /// </summary>
    public class BaseEvent : IEvent
    {
        public BaseEvent(Guid id, DateTime createDateTime)
        {
            Id = id;
            CreateDateTime = createDateTime;        
        }

        public BaseEvent()
        {

        }

        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid Id { set; get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDateTime { set; get; }
     
    }
}
