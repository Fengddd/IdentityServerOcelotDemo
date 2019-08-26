using System;
using System.Collections.Generic;
using System.Text;
using ProjectCore.Common.DomainInterfaces;

namespace ProjectCore.Common.Event
{
    public interface IAggregateRootWithEventSourcing : IAggregationRoot
    {
        IEnumerable<IDomainEvent> UncommittedEvents { get; }

        void Replay(IEnumerable<IDomainEvent> events);

        long Version { get; }
    }
}
