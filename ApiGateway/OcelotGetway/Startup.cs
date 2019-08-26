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
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;

namespace OcelotGetway
{
    /// <summary>
    /// 有多个路由的配置文件时使用ocelot合并配置文件，已ocelot开头例如：ocelot.First.json ,自动合并在ocelot.json中
    /// ocelot合并配置文件，不能出现同样的一个端口，多个路由的情况
    /// </summary>
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAuthentication()
                .AddIdentityServerAuthentication("IdentityServerKey", options =>
                {
                    options.Authority = "http://localhost:17491";
                    options.ApiName = "identityServerApi";
                    options.SupportedTokens = IdentityServer4.AccessTokenValidation.SupportedTokens.Both;
                    options.RequireHttpsMetadata = false;
                });

            services.AddOcelot()
                .AddConsul()
                .AddPolly();

            //配置跨域处理
            services.AddCors(options =>
            {
                options.AddPolicy("any", builder =>
                {
                    builder.AllowAnyOrigin() //允许任何来源的主机访问
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();//指定处理cookie
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //配置Cors
            app.UseCors("any");
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseOcelot();
            app.UseMvc();
        }
    }
}
