using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ProjectCore.Common.Dapper;

namespace ProjectCore.Common.Event.CQRS
{
    /// <summary>
    /// 事件存储
    /// </summary>
    public class EventStorage : IEventStorage
    {
        /// <summary>
        /// 事件存储保存
        /// </summary>
        /// <param name="event"></param>
        public async Task EventStorageSava(IEvent @event)
        {
            string connectionString = "Data Source=DESKTOP-UQ93D9H;Initial Catalog=MyNetCoreProject;User ID=sa;Password=123456li";
            SqlConnection conn = new SqlConnection(connectionString);
            await conn.ExecuteAsync("INSERT INTO dbo.EventStorageInfo VALUES ('"+Guid.NewGuid()+"',@CreateDateTime,@AggregationRootId,@AssemblyQualifiedAggreateRooType,@AssemblyQualifiedCommandAndEventType,@Version,@EventData)", @event);
        }
    }
}
