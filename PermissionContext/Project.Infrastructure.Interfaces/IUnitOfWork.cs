
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCore.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        void Commit();
        Task CommitAsync();       
    }
}
