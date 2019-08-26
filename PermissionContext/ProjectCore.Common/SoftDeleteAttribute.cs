using System;

namespace ProjectCore.Common
{
    /// <summary>
    /// 软删除特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class SoftDeleteAttribute : Attribute
    {
    }
}
