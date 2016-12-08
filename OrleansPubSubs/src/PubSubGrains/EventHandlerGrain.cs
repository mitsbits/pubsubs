using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PubSubContracts;

namespace PubSubGrains
{
   public class EventHandlerGrain :  IEventHandler
    {
        public void Handle(string msg)
        {
            Console.WriteLine($"Handling {msg}");
            
        }
    }
}
