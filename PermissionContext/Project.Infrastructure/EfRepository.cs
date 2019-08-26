using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectCore.Infrastructure.Interfaces;

namespace ProjectCore.Infrastructure
{
    public class EfRepository : IRepository
    {
        private readonly MyContext _dbContext;
        public EfRepository(MyContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// 提交
        /// </summary>
        public void Commit()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {                
                throw new ArgumentException(e.Message);
            }
        }
        /// <summary>
        /// 异步提交
        /// </summary>
        /// <returns></returns>
        public async Task CommitAsync()
        {
            try
            {
               await _dbContext.SaveChangesAsync(CancellationToken.None);
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }
        /// <summary>
        /// 内存回收
        /// </summary>
        public void Dispose()
        {
            _dbContext.Dispose();
            GC.Collect();
        }
    }
}
