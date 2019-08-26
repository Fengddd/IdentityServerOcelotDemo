using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCore.Common.Event
{
    /// <summary>
    /// 领域事件
    /// </summary>
    public class DomainEvent : IDomainEvent
    {
        protected DomainEvent()
        {
            this.Id = Guid.NewGuid();
            this.CreateDateTime = DateTime.Now;
        }
        public Guid Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public object AggregateRootId { get; set; }
        public string AggregateRootType { get; set; }
        public long Version { get; set; }
    }
}
