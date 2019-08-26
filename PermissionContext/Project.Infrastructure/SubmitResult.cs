using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCore.Infrastructure
{
    /// <summary>
    /// 应用层返回参数
    /// </summary>
   public class AppServiceResult
    {
        public virtual bool IsSucceed { get; set; }

        public virtual string Message { get; set; }
    }
}
