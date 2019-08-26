using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCore.Common.Event
{
    /// <summary>
    /// 事件总线
    /// </summary>
    public interface IEventBus: IEventPublisher,IEventSubscribe
    {

    }
}
