using Autofac;
using Autofac.Configuration;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectCore.Common.IocHelper;
using ProjectCore.Domain.DomainService;
using ProjectCore.Domain.Repository.Interfaces;
using ProjectCore.EntityFrameworkCore;
using ProjectCore.Infrastructure.Repository;
using ProjectCore.WebApi.Filter;

namespace ProjectCore.WebApi
{
    public static class DependencyInjectionConfig
    {
        //依赖注入的属性
        public static Autofac.IContainer Container { get; private set; }
        public static AutofacServiceProvider Configure(this IServiceCollection services, string connectionString, IConfigurationRoot configuration)
        {          
            services.AddDbContext<MyContext>(options => options.UseSqlServer(connectionString));
            //注入
            DependencyInjectionService.GetInstance(services)
                .AddMvc()
                .AddCookie()
                .AddSwagger()
                .AddCors()
                .AddDddLayering()
                .AddEventBus()
                .AddCap();
                //.AddIdentityServer();
            //Autofac依赖注入 Class的后面名字必须一致才能注入
            var builder = new ContainerBuilder();
            builder.Populate(services);
            var module = new ConfigurationModule(configuration);
            builder.RegisterModule(module);
            Container = builder.Build();
            return new AutofacServiceProvider(Container);
        }
    }
}
