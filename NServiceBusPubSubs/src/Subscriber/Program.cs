using NServiceBus;
using NServiceBus.Features;
using Shared;
using System;
using System.Threading.Tasks;

namespace Subscriber
{
    public class Program
    {
        private static void Main()
        {
            AsyncMain().GetAwaiter().GetResult();
        }

        private static async Task AsyncMain()
        {
            Console.Title = "Samples.StepByStep.Subscriber";
            var endpointConfiguration = new EndpointConfiguration("Samples.StepByStep.Subscriber");
            endpointConfiguration.UseSerialization<JsonSerializer>();
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.DisableFeature<AutoSubscribe>();
            endpointConfiguration.CustomConfigurationSource(new ConfigurationSource());

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);
            await endpointInstance.Subscribe<OrderPlaced>()
                .ConfigureAwait(false);

            try
            {
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
            finally
            {
                await endpointInstance.Unsubscribe<OrderPlaced>()
                    .ConfigureAwait(false);
                await endpointInstance.Stop()
                    .ConfigureAwait(false);
            }
        }
    }
}