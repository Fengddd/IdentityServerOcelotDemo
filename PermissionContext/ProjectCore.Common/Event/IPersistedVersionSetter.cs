using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCore.Common.Event
{
    /// <summary>
    /// IPersistedVersionSetter接口允许调用者对聚合的“保存版本号”进行设置。
    /// 这个版本号表示了在事件存储中，属于当前聚合的所有事件的个数。
    /// 试想，如果一个聚合的“保存版本号”为4（即在事件存储中有4个事件是属于该聚合的），
    /// 那么，如果再有2个事件发生在这个聚合中，于是，该聚合的版本就是4+2=6.
    /// </summary>
    public interface IPersistedVersionSetter
    {
        long PersistedVersion { set; }
    }
}
