using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ProjectCore.Common.Event
{
   public class CeShiAggregate: AggregateRootWithEventSourcing<Guid>
    {

        public CeShiAggregate(Guid id) : base(id)
        {

        }

        public void ChangeTitle(string title)
        {
            ApplyEvent(new CeshiAggregateEvent(title));
        }

        public string CeShiTitle { get;private set; }

        private void Handle(CeshiAggregateEvent @event)
        {
            this.CeShiTitle = @event.CeshiName;
        }
    }
}
