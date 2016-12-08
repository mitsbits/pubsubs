using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace PubSubContracts
{
    public interface IPubSubGrain : IGrainWithIntegerKey
    {
        Task Subscribe(IEventHandler observer);
        Task Publish(string message);
        Task UnSubscribe(IEventHandler observer);
    }
}
