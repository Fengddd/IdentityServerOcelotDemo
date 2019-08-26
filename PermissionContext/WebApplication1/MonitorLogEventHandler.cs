using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ProjectCore.Common.Event;

namespace WebApplication1
{
    public class MonitorLogEventHandler : IEventHandler
    {
        public async Task<bool> HandlerAsync<TEvent>(TEvent @event) where TEvent : IEvent
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-0022CQ5\\LI;Initial Catalog=MyNetCoreProject;User ID=sa;Password=123456;MultipleActiveResultSets=true";
                //var table = SqlHelper.GetTable(connectionString, CommandType.Text, "select * from EventStorageInfo");
                SqlConnection conn = new SqlConnection(connectionString);
                string sql = "INSERT INTO MonitorLogInfo(MonitorLogId,ControllerName,ActionName,RequestParameters,ExecuteStartTime,ExecuteEndTime,ExecutionTime,ErrorMsg,LogType,LogLevel,AddressIp,UserId,UserName) VALUES(@MonitorLogId,@ControllerName,@ActionName,@RequestParameters,@ExecuteStartTime,@ExecuteEndTime,@ExecutionTime,@ErrorMsg,@LogType,@LogLevel,@AddressIp,@UserId,@UserName)";
                await conn.ExecuteAsync(sql, @event);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
