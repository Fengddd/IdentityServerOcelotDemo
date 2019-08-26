using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ProjectCore.Common.Event
{
    /// <summary>
    /// 事件和事件处理注册
    /// </summary>
    public class EventHandlerRegister : IEventHandlerRegister
    {
        private readonly IServiceCollection _registry;
        private readonly IServiceProvider _serviceprovider;
        Dictionary<Type,List<Type>> dictionarys=new Dictionary<Type, List<Type>>();
        public EventHandlerRegister(IServiceCollection registry, Func<IServiceCollection, IServiceProvider> serviceProviderFactory = null)
        {
            this._registry = registry;
            this._serviceprovider =_registry.BuildServiceProvider();
        }
        /// <summary>
        /// 事件处理
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="event"></param>
        /// <returns></returns>
        public async Task HandlerAsync<TEvent>(TEvent @event) where TEvent : IEvent
        {
            var eventType = @event.GetType();
            if (dictionarys.TryGetValue(eventType, out List<Type> handlerTypes) && handlerTypes.Count > 0)
            {
                using (var childScope = _serviceprovider.CreateScope())
                {
                    foreach (var handlerType in handlerTypes)
                    {
                        var handler = (IEventHandler) Activator.CreateInstance(handlerType);
                        if (handler != null)
                            await handler.HandlerAsync(@event);
                    }
                }
            }
        }

        /// <summary>
        /// 事件是否注册
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <typeparam name="TEventHandler"></typeparam>
        /// <returns></returns>
        public bool IsRegister<TEvent, TEventHandler>() where TEvent : IEvent where TEventHandler : IEventHandler
        {
            var eventType = typeof(TEvent);
            if (dictionarys.TryGetValue(eventType, out List<Type> handlers))
            {
                return handlers != null && handlers.Contains(typeof(IEventHandler));
            }
            return false;
        }

        /// <summary>
        /// 事件注册
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <typeparam name="TEventHandler"></typeparam>
        /// <returns></returns>
        public void Register<TEvent, TEventHandler>() where TEvent : IEvent where TEventHandler : IEventHandler
        {
            var eventType = typeof(TEvent);
            var eventHandlerType = typeof(TEventHandler);

            DictionaryRguster.DictionaryRgustered(eventType, eventHandlerType, this.dictionarys);
        }
    }
}
