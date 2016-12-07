using NServiceBus;
using NServiceBus.Logging;
using Shared;
using System.Threading.Tasks;

namespace Subscriber
{
    public class OrderCreatedHandler :
        IHandleMessages<OrderPlaced>
    {
        private static ILog log = LogManager.GetLogger<OrderCreatedHandler>();

        public Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            log.Info($"Handling: OrderPlaced for Order Id: {message.OrderId}");
            return Task.CompletedTask;
        }
    }
}