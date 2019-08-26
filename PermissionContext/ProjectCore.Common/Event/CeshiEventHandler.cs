using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;
using ProjectCore.Common.Dapper;
using ProjectCore.Common.Event.CQRS;
using TCSCD.Unity;

namespace ProjectCore.Common.Event
{
    public class CeshiEventHandler : IEventHandler
    {

        public CeshiEventHandler()
        {
           
        }
     

        public async Task<bool> HandlerAsync<TEvent>(TEvent @event) where TEvent : IEvent
        {
            try
            {
                //var ceshiEvent = @event as CeshiEvent;
                //var storage = new CeshiEvent();

                //storage.CeshiName = ceshiEvent.CeshiName;
             
                //storage.CreateDateTime = ceshiEvent.CreateDateTime;

                //await new EventStorage().EventStorageSava(storage);
               Debug.Write("ddd");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return true;
        }
    }
}
