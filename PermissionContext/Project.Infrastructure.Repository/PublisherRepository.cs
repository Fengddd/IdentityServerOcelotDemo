using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ProjectCore.Common.Event;

namespace ProjectCore.Infrastructure.Repository
{
    public static class PublisherRepository
    {
        public static void publisher<T>(T @enent)
        {
            IEnumerable<IDomainEvent> list = (IEnumerable<IDomainEvent>) @enent;


        }
    }
}
