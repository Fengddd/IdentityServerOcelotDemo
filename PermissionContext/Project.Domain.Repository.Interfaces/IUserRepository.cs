﻿using System;
using System.Collections.Generic;
using System.Text;
using ProjectCore.Domain.Model.Entity;

namespace ProjectCore.Domain.Repository.Interfaces
{
    /// <summary>
    /// 用户
    /// </summary>
    public interface IUserRepository:IBaseRepository<UserInfo>
    {
         List<UserInfo> UserList();
    }
}
