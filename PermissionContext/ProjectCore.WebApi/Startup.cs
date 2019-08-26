using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using log4net;
using log4net.Config;
using log4net.Repository;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProjectCore.Common;
using ProjectCore.Common.Event;
using ProjectCore.Common.RabbitMqEvent;
using ProjectCore.EntityFrameworkCore;
using ProjectCore.WebApi.Filter;
using RabbitMQ.Client;
using Swashbuckle.AspNetCore.Swagger;


namespace ProjectCore.WebApi
{
    public class Startup
    {
        public static ILoggerRepository Repository { get; set; }
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            //Configuration = configuration;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                //.AddJsonFile("autofac.json")//读取autofac.json文件
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            Repository = LogManager.CreateRepository("NETCoreRepository");
            XmlConfigurator.Configure(Repository, new FileInfo("log4net.config"));
        }
       
        //public IConfiguration Configuration { get; }
        public IConfigurationRoot Configuration { get; }
        //依赖注入的属性
        public IContainer Container { get; private set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //这里就是填写数据库的链接字符串          
            var connection = Configuration.GetSection("ConnectionService")["ConnectionSqlService"];                        
            return services.Configure(connection, this.Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMiniProfiler();
            //启用中间件服务生成Swagger作为JSON终结点
            app.UseSwagger();
            //启用中间件服务对swagger-ui，指定Swagger JSON终结点
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

            });
            //配置Cors
            app.UseCors("any");
            //app.UseHttpsRedirection();
            app.UseAuthentication();
            // CAP
            app.UseCap();

            app.UseMvc();
          
            appLifetime.ApplicationStopped.Register(() => this.Container.Dispose());
            
        }
    }
}
