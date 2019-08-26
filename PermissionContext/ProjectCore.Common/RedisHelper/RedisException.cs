using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCore.Common.RedisHelper
{
    /// <summary>
    /// Redis 异常错误处理
    /// </summary>
    public class RedisException:Exception
    {
        public RedisException(string msg) :base(msg) {

        }
    }
}
