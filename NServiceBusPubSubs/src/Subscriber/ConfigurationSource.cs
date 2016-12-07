using NServiceBus.Config;

namespace Subscriber
{
    public class ConfigurationSource :
        NServiceBus.Config.ConfigurationSource.IConfigurationSource
    {
        public T GetConfiguration<T>() where T : class, new()
        {
            var config = new UnicastBusConfig()
            {
                MessageEndpointMappings = new MessageEndpointMappingCollection
                {
                    new MessageEndpointMapping {AssemblyName = "Shared", Endpoint = "Samples.StepByStep.Server" }
                }
            };

            return config as T;
        }
    }
}