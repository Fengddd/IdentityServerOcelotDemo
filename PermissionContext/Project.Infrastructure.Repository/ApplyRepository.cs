using System;
using System.Collections.Generic;
using System.Text;
using ProjectCore.Domain.Repository.Interfaces;
using ProjectCore.EntityFrameworkCore;


namespace ProjectCore.Infrastructure.Repository
{
   public class ApplyRepository<T>:BaseRepository<T>,IApplyRepository<T> where T:class,new ()
    {
        public ApplyRepository(MyContext context) : base(context)
        {

        }

       
    }
}
