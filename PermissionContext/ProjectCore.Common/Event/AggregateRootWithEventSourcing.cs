using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProjectCore.Common.DomainInterfaces;
using ProjectCore.Common.IocHelper;
using Unity;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using DotNetCore.CAP;

namespace ProjectCore.Common.Event
{
    public class AggregateRootWithEventSourcing<TAggregateRoot> : IAggregateRootWithEventSourcing
    {
        #region Private Fields

        private readonly Lazy<Dictionary<string, MethodInfo>> registeredHandlers;
        private readonly Queue<IDomainEvent> uncommittedEvents = new Queue<IDomainEvent>();
        private long persistedVersion = 0;      
        private object sync = new object();
        //private IMediator _mediator;
        //private static IContainer Container { get; set; }
        //private IServiceCollection _registry;
        //private readonly ICapPublisher _capBus;
        #endregion Private Fields
        public TAggregateRoot AggregateRootId { get;private set; }
        //public void MediatorInit()
        //{
        //    _registry = new ServiceCollection();
        //    _registry.AddMediatR();
        //    var builder = new ContainerBuilder();
        //    builder.Populate(_registry);
        //    Container = builder.Build();
        //    var autofacServiceProvider = new AutofacServiceProvider(Container);
        //    _mediator = Container.Resolve<IMediator>();
        //}

        #region Protected Constructors

        public AggregateRootWithEventSourcing(Guid id)
        {  
            registeredHandlers = new Lazy<Dictionary<string, MethodInfo>>(() =>
            {              
                var registry = new Dictionary<string, MethodInfo>();
                var methodInfoList = from mi in this.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                                     let returnType = mi.ReturnType
                                     let parameters = mi.GetParameters()
                                     where mi.Name == "Handle" &&
                                     returnType == typeof(void) &&
                                     parameters.Length == 1 &&
                                     typeof(IDomainEvent).IsAssignableFrom(parameters[0].ParameterType)
                                     select new { EventName = parameters[0].ParameterType.FullName, MethodInfo = mi };

                foreach (var methodInfo in methodInfoList)
                {
                    registry.Add(methodInfo.EventName, methodInfo.MethodInfo);
                }         
                return registry;
            });


        }

        public AggregateRootWithEventSourcing(TAggregateRoot aggregateRootId)
        {
            if ((object)aggregateRootId == null)
                throw new ArgumentNullException(nameof(aggregateRootId));
            this.AggregateRootId = aggregateRootId;
        }

        #endregion Protected Constructors

        #region Public Properties

        long PersistedVersion { set => Interlocked.Exchange(ref this.persistedVersion, value); }

        public IEnumerable<IDomainEvent> UncommittedEvents => uncommittedEvents;

        public long Version => this.uncommittedEvents.Count + this.persistedVersion;

        Guid IEntity.Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        #endregion Public Properties

        public void Purge()
        {
            lock (sync)
            {
                uncommittedEvents.Clear();
            }
        }

        public void Replay(IEnumerable<IDomainEvent> events)
        {
            ((IPurgable)this).Purge();
            events.OrderBy(e => e.CreateDateTime)
                .ToList()
                .ForEach(e =>
                {
                    HandleEvent(e);
                    Interlocked.Increment(ref this.persistedVersion);
                });
        }

        #region Protected Methods
        protected void ApplyEvent<TDomainEvent>(TDomainEvent domainEvent)
            where TDomainEvent : IDomainEvent
        {
            lock (sync)
            {
                // 然后设置事件的元数据，包括当前事件所对应的聚合根类型以及
                // 聚合的ID值。
                domainEvent.AggregateRootId = AggregateRootId;
                domainEvent.AggregateRootType = this.GetType().AssemblyQualifiedName;
                domainEvent.Version = this.Version + 1;
                // 首先处理事件数据。
                this.HandleEvent(domainEvent);
                // 最后将事件缓存在“未提交事件”列表中。
                this.uncommittedEvents.Enqueue(domainEvent);
            }      
        }

        #endregion Protected Methods

        #region Private Methods

        private void HandleEvent<TDomainEvent>(TDomainEvent domainEvent)
            where TDomainEvent : IDomainEvent
        {
            var key = domainEvent.GetType().FullName;
            if (registeredHandlers.Value.ContainsKey(key))
            {
                registeredHandlers.Value[key].Invoke(this, new object[] { domainEvent });
            }
        }

        #endregion Private Methods
    }
}
