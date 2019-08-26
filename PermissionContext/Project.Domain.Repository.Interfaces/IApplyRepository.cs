using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCore.Domain.Repository.Interfaces
{
   public interface IApplyRepository<T>:IBaseRepository<T> where T:class,new ()
   {

   }
}
