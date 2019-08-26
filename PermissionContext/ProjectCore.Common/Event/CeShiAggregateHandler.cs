using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace ProjectCore.Common.Event
{
    public class CeShiAggregateHandler: INotificationHandler<CeshiAggregateEvent>
    {
        public Task Handle(CeshiAggregateEvent notification, CancellationToken cancellationToken)
        {
            Debug.WriteLine("Pong 1");
            return Task.CompletedTask;
        }

    }
}
