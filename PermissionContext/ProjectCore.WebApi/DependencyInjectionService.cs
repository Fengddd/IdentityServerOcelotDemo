using System;
using System.IO;
using System.Text;
using Autofac;
using Autofac.Configuration;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProjectCore.Common;
using ProjectCore.Common.Event;
using ProjectCore.Common.IocHelper;
using ProjectCore.Common.RabbitMqEvent;
using ProjectCore.Domain.Repository.Interfaces;
using ProjectCore.EntityFrameworkCore;
using ProjectCore.Infrastructure.Repository;
using ProjectCore.WebApi.Filter;
using RabbitMQ.Client;
using Swashbuckle.AspNetCore.Swagger;

namespace ProjectCore.WebApi
{
    /// <summary>
    /// 依赖注入配置
    /// </summary>
    public class DependencyInjectionService
    {
        private static DependencyInjectionService _dependencyInjectionConfiguration;
        private static readonly object LockObj = new object();
        private static IServiceCollection _services;

        /// <summary>
        /// 单例模式
        /// </summary>
        /// <returns></returns>
        public static DependencyInjectionService GetInstance(IServiceCollection services)
        {
            _services = services;
            if (_dependencyInjectionConfiguration == null)
            {
                lock (LockObj)
                {
                    if (_dependencyInjectionConfiguration == null)
                    {
                        _dependencyInjectionConfiguration = new DependencyInjectionService();
                    }
                }
            }
            return _dependencyInjectionConfiguration;
        }

        /// <summary>
        /// 添加MVC
        /// </summary>
        /// <returns></returns>
        public DependencyInjectionService AddMvc()
        {
            _services.AddMvc(options =>
                {
                    options.Filters.Add(typeof(Log4Helper));
                }).
                SetCompatibilityVersion(CompatibilityVersion.Version_2_1).
                AddJsonOptions(options =>
                {
                    //忽略循环引用
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    //不使用驼峰样式的key
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    //设置时间格式
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
                });
            return _dependencyInjectionConfiguration;
        }

        /// <summary>
        /// 添加Cookie
        /// </summary>
        /// <returns></returns>
        public DependencyInjectionService AddCookie()
        {
            //注册Cookie认证服务
            _services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            return _dependencyInjectionConfiguration;
        }

        /// <summary>
        /// 添加jwt
        /// </summary>
        /// <returns></returns>
        public DependencyInjectionService AddJwt()
        {
            ////将appsettings.json中的JwtSettings部分文件读取到JwtSettings中，这是给其他地方用的
            //_services.Configure<JwtSettings>(AutoMapper.Configuration.IConfiguration.GetSection("JwtSettings"));
            ////将配置绑定到JwtSettings实例中
            //var jwtSettings = new JwtSettings();
            //IConfiguration.Bind("JwtSettings", jwtSettings);
            //_services.AddAuthentication(options =>
            //    {
            //        //认证middleware配置
            //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    })
            //    .AddJwtBearer(o =>
            //    {
            //        //主要是jwt  token参数设置
            //        o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            //        {
            //            //Token颁发机构
            //            ValidIssuer = jwtSettings.Issuer,
            //            //颁发给谁
            //            ValidAudience = jwtSettings.Audience,
            //            //这里的key要进行加密，需要引用Microsoft.IdentityModel.Tokens
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
            //            ValidateIssuerSigningKey = true,
            //            ////是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
            //            ValidateLifetime = true,
            //            ////允许的服务器时间偏移量
            //            //ClockSkew=TimeSpan.Zero

            //        };
            //    });
            return _dependencyInjectionConfiguration;
        }

        /// <summary>
        /// 添加CAP
        /// </summary>
        /// <returns></returns>
        public DependencyInjectionService AddCap()
        {
            _services.AddCap(x =>
            {
                // 如果你的 SqlServer 使用的 EF 进行数据操作，你需要添加如下配置： 
                x.UseEntityFramework<MyContext>();
                //启用操作面板
                x.UseDashboard();
                // 如果你使用的 RabbitMQ 作为MQ，你需要添加如下配置：              
                x.UseRabbitMQ(cfg =>
                {
                    cfg.HostName = "127.0.0.1";
                    cfg.UserName = "guest";
                    cfg.Password = "guest";
                });
                //设置失败重试次数
                x.FailedRetryCount = 5;
            });
            return _dependencyInjectionConfiguration;
        }

        /// <summary>
        /// 添加跨域
        /// </summary>
        /// <returns></returns>
        public DependencyInjectionService AddCors()
        {
            //配置跨域处理
            _services.AddCors(options =>
            {
                options.AddPolicy("any", builder =>
                {
                    builder.AllowAnyOrigin() //允许任何来源的主机访问
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();//指定处理cookie
                });
            });
            return _dependencyInjectionConfiguration;
        }

        /// <summary>
        /// 添加Swagger
        /// </summary>
        /// <returns></returns>
        public DependencyInjectionService AddSwagger()
        {
            //注册Swagger生成器，定义一个和多个Swagger 文档
            _services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "API",
                    Description = "权限管理",
                    TermsOfService = "None"

                });
                //swagger中控制请求的时候发是否需要在url中增加accesstoken
                c.OperationFilter<HttpHeaderFilter>();

                // 为 Swagger JSON and UI设置xml文档注释路径
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var xmlPath = Path.Combine(basePath, "WebApiSwagger.xml");
                c.IncludeXmlComments(xmlPath);

            });
            return _dependencyInjectionConfiguration;
        }

        /// <summary>
        /// 添加事件总线
        /// </summary>
        /// <returns></returns>
        public DependencyInjectionService AddEventBus()
        {
            //定义要发布消息的事件总线
            _services.AddSingleton<IEventHandlerRegister>(new EventHandlerRegister(_services));
            var connectionFactory = new ConnectionFactory
            {
                UserName = "guest",//用户名
                Password = "guest",//密码
                HostName = "localhost"
            };
            _services.AddSingleton<IApplicationEventBus>(sp => new ApplicationEventBus(sp.GetRequiredService<IEventHandlerRegister>(), connectionFactory,
                "direct", true));
            return _dependencyInjectionConfiguration;
        }

        /// <summary>
        /// 添加DDD分层的注入
        /// </summary>
        /// <returns></returns>
        public DependencyInjectionService AddDddLayering()
        {
            _services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            //services.AddScoped(typeof(LoginDomainService));
            //通过反射进行依赖注入不能为空
            _services.RegisterAssembly("ProjectCore.Domain.Repository.Interfaces", "ProjectCore.Infrastructure.Repository");
            _services.RegisterAssembly("ProjectCore.Application");
            _services.RegisterDomainServiceAssembly("ProjectCore.Domain.DomainService");
     
            return _dependencyInjectionConfiguration;
        }

        /// <summary>
        /// 添加IdentityServer验证
        /// </summary>
        /// <returns></returns>
        public DependencyInjectionService AddIdentityServer()
        {
            _services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://localhost:17491";
                    options.RequireHttpsMetadata = false;
                    //设置token过期时间
                    options.TokenValidationParameters.ClockSkew = TimeSpan.FromSeconds(0);
                    options.TokenValidationParameters.RequireExpirationTime = true;
                    options.Audience = "identityServerApi";

                });
            return _dependencyInjectionConfiguration;
        }

    }
}
