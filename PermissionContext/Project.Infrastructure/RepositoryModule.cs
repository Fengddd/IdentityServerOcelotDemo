using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Autofac;
using Module = Autofac.Module;

namespace ProjectCore.Infrastructure
{
    /// <summary>
    /// Autofac 程序集注入
    /// </summary>
    public class RepositoryModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<RepositorySpeck>().As<IRepositorySpeck>();
            //builder.RegisterType<DailyRepository>().As<IDailyRepository>();
            //builder.RegisterType<MonthlyRepository>().As<IMontylyRepository>();
            //builder.RegisterType<YearlyRepository>().As<IYearlyRepository>();

            //builder.RegisterAssemblyTypes(this.ThisAssembly)
            //    .Where(t => t.Name.EndsWith("Repository<T>"))
            //    .AsImplementedInterfaces()
            //    .InstancePerLifetimeScope();
            

        }
    }
}
