using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCore.Common.Event
{
    /// <summary>
    /// IPurgable表示，实现了该接口的类型具有某种清空操作，比如清空某个队列，
    /// 或者将对象状态恢复到初始状态。让IAggregateRootWithEventSourcing继承于该接口是因为，
    /// 当仓储完成了聚合中领域事件的保存和派发之后，需要清空聚合中缓存的事件，以保证在今后，
    /// 发生在同一时间点的同样的事件不会被再次保存和派发
    /// </summary>
    public interface IPurgable
    {
        void Purge();
    }
}
