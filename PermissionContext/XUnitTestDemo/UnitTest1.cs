using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Dapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using ProjectCore.Common.Dapper;
using ProjectCore.Common.DomainInterfaces;
using ProjectCore.Common.Event;
using ProjectCore.Common.IocHelper;
using ProjectCore.Common.RabbitMqEvent;
using ProjectCore.Common.RedisHelper;
using ProjectCore.Domain.Repository.Interfaces;
using ProjectCore.EntityFrameworkCore;
using ProjectCore.Infrastructure.Repository;
using RabbitMQ.Client;
using Unity;
using Xunit;

namespace XUnitTestDemo
{
    public class UnitTest1
    {
        private readonly IEventBus _eventBus;
        private readonly IMediator _mediator;
        private static IContainer Container { get; set; }
        private readonly IServiceCollection _registry;
        public UnitTest1()
        {
            _registry = new ServiceCollection();
            //定义要发布消息的事件总线
            _registry.AddSingleton<IEventHandlerRegister>(new EventHandlerRegister(_registry));
            var connectionFactory = new ConnectionFactory
            {
                UserName = "guest",//用户名
                Password = "guest",//密码
                HostName = "localhost"
            };
            _registry.AddSingleton<IApplicationEventBus>(sp => new ApplicationEventBus(sp.GetRequiredService<IEventHandlerRegister>(), connectionFactory,
                "direct", true));
            _registry.AddScoped(typeof(IAggregateRootWithEventSourcing), typeof(AggregateRootWithEventSourcing<>));

            //services.RegisterAssembly("ProjectCore.Domain.Repository.Interfaces", "ProjectCore.Infrastructure.Repository");
            //services.RegisterAssembly("ProjectCore.Application");
            //services.RegisterAssembly("ProjectCore.Domain.DomainService");
            //services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            //services.AddScoped<IMyContext, MyContext>();
            //services.AddDbContext<MyContext>(options => options.UseSqlServer("Data Source=DESKTOP-0022CQ5\\LI;Initial Catalog=MyNetCoreProject;User ID=sa;Password=123456;MultipleActiveResultSets=true"));
            //Autofac依赖注入 Class的后面名字必须一致才能注入
            var builder = new ContainerBuilder();
            builder.Populate(_registry);
            Container = builder.Build();
            var c = new AutofacServiceProvider(Container);
            _eventBus = Container.Resolve<IApplicationEventBus>();

        }

        [Fact]
        public void CeShi()
        {
            _eventBus.Subscribe<CeshiEvent, CeshiEventHandler>();
        }

        [Fact]
        public void AddEventStorage()
        {
            string connectionString = "Data Source=DESKTOP-UQ93D9H;Initial Catalog=MyNetCoreProject;User ID=sa;Password=123456li";
            //var table = SqlHelper.GetTable(connectionString, CommandType.Text, "select * from EventStorageInfo");
            SqlConnection conn = new SqlConnection(connectionString);
            var a = conn.Query<BaseEvent>("select * from EventStorageInfo");

            var sql = "INSERT INTO dbo.EventStorageInfo(Id,CreateDateTime,AggregationRootId,AssemblyQualifiedAggreateRooType,AssemblyQualifiedCommandAndEventType,Version,EventData)VALUES(NEWID(),SYSDATETIME(),NEWID(),N'55',N'55', 1,NULL)";
            SqlHelper.ExecteNonQuery(connectionString, CommandType.Text, sql);
        }

        [Fact]
        public void CeShiTitle()
        {
            CeShiAggregate ce = new CeShiAggregate(Guid.NewGuid());
            ce.ChangeTitle("李锋");
            var c = ce.UncommittedEvents.Count();
            PublisherRepository.publisher(ce.UncommittedEvents);
            Assert.True(c > 0);
        }

        [Fact]
        public async Task CeShiMediator()
        {
            await _mediator.Publish(new CeshiAggregateEvent("ddd"));
        }

        [Fact]
        public void CeShiRedisPoliy()
        {
            RedisManager.Instance();
        }

    }
}
