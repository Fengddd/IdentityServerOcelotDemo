using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCore.Common.Event
{
    public class CeshiEvent : BaseEvent
    {
        public string CeshiName { get; set; }
        public CeshiEvent()
        {
          
        }
        public CeshiEvent(string ceshiName)
        {
            CeshiName = ceshiName;
        }
    }
}
