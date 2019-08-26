using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProjectCore.Common.Event;
using ProjectCore.Common.Log;
using ProjectCore.Common.RabbitMqEvent;
using RabbitMQ.Client;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //定义要发布消息的事件总线
            services.AddSingleton<IEventHandlerRegister>(new EventHandlerRegister(services));
            var connectionFactory = new ConnectionFactory
            {
                UserName = "guest",//用户名
                Password = "guest",//密码
                HostName = "localhost"
            };
            services.AddSingleton<IApplicationEventBus>(sp => new ApplicationEventBus(sp.GetRequiredService<IEventHandlerRegister>(), connectionFactory,
                "direct", true));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //订阅创建订单命令
            var commandbuss = app.ApplicationServices.GetService<IApplicationEventBus>();
            commandbuss.Subscribe<MonitorLogEvent, MonitorLogEventHandler>();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
