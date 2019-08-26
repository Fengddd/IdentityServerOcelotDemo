using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCore.Infrastructure.Interfaces
{
   public interface IEntity
   {
       /// <summary>
       /// 实体Id
       /// </summary>
       Guid Id { get; set; }
       /// <summary>
       /// 实体的标识
       /// </summary>
       string Code { get; set; }
   }
}
