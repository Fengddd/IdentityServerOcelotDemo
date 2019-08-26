using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ProjectCore.Infrastructure
{
    public class DbContextFactory : IDesignTimeDbContextFactory<MyContext>
    {      
        public MyContext CreateDbContext(string[] args)
        {         
            var builder = new DbContextOptionsBuilder<MyContext>();
            //读取appsettings.json的数据库连接字符串
            var connection = JsonConfigurationHelper.GetAppSettings<GetConnectionSqlService>("appsettings.json", "GetConnectionSqlService");

            builder.UseSqlServer(connection.ConnectionSqlService);
          
            return new MyContext(builder.Options);
        }

    }
}
