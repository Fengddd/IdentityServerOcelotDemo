using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCore.Common.Event
{
   public class CeshiAggregateEvent:DomainEvent, INotification
    {
        public string CeshiName { get; set; }
        public CeshiAggregateEvent()
        {

        }
        public CeshiAggregateEvent(string ceshiName)
        {
            CeshiName = ceshiName;
        }
    }
}
