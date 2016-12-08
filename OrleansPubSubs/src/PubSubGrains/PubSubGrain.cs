using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans;
using PubSubContracts;

namespace PubSubGrains
{
    public class PubSubGrain : Grain, IPubSubGrain
    {
        private ObserverSubscriptionManager<IEventHandler> _subsManager;

        public override async Task OnActivateAsync()
        {
            // We created the utility at activation time.
            _subsManager = new ObserverSubscriptionManager<IEventHandler>();
            await base.OnActivateAsync();
        }

        // Clients call this to subscribe.
        public async Task Subscribe(IEventHandler observer)
        {
            _subsManager.Subscribe(observer);
        }

        public Task Publish(string message)
        {
            _subsManager.Notify(s => s.Handle(message));
            return TaskDone.Done;
        }

        //Also clients use this to unsubscribe themselves to no longer receive the messages.
        public async Task UnSubscribe(IEventHandler observer)
        {
            _subsManager.Unsubscribe(observer);
        }
    }
}
