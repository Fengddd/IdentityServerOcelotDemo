using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectCore.Domain.Model.Entity;
using ProjectCore.Domain.Repository.Interfaces;
using ProjectCore.EntityFrameworkCore;

namespace ProjectCore.Infrastructure.Repository
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserRepository:BaseRepository<UserInfo>, IUserRepository
    {
        public UserRepository(MyContext myContext) : base(myContext)
        {
           
        }

        public List<UserInfo> UserList()
        {
            return _dbContext.UserInfo.Take(10).ToList();
        }
    }
}
