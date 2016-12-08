using System;
using System.Threading.Tasks;

using Orleans;
using Orleans.Runtime.Configuration;
using Orleans.Runtime.Host;
using PubSubContracts;
using PubSubGrains;

namespace PubSubSilo
{
    /// <summary>
    /// Orleans test silo host
    /// </summary>
    public class Program
    {
        static SiloHost siloHost;

        static void Main(string[] args)
        {
            Console.WriteLine("Waiting for Orleans Silo to start. Press Enter to proceed...");
            Console.ReadLine();

            // Orleans comes with a rich XML and programmatic configuration. Here we're just going to set up with basic programmatic config
            var config = Orleans.Runtime.Configuration.ClientConfiguration.LocalhostSilo(30000);

            GrainClient.Initialize("ClientConfiguration.xml");

            //First create the grain reference
            var friend = GrainClient.GrainFactory.GetGrain<IPubSubGrain>(0);
            EventHandlerGrain c = new EventHandlerGrain();

            //Create a reference for chat usable for subscribing to the observable grain.
            var obj = GrainClient.GrainFactory.CreateObjectReference<IEventHandler>(c).Result;
            //Subscribe the instance to receive messages.
            Task.WaitAll(friend.Subscribe(obj));
            friend.Publish("Yelloooooo");

            Console.ReadLine();

        }
        static void DoSomeClientWork()
        {
            // Orleans comes with a rich XML and programmatic configuration. Here we're just going to set up with basic programmatic config
            var config = Orleans.Runtime.Configuration.ClientConfiguration.LocalhostSilo(30000);
            GrainClient.Initialize(config);

            var friend = GrainClient.GrainFactory.GetGrain<IHello>(0);
            var result = friend.SayHello("Goodbye").Result;
            Console.WriteLine(result);

        }
        static void InitSilo(string[] args)
        {
            siloHost = new SiloHost(System.Net.Dns.GetHostName());
            // The Cluster config is quirky and weird to configure in code, so we're going to use a config file
            siloHost.ConfigFileName = "OrleansConfiguration.xml";

            siloHost.InitializeOrleansSilo();
            var startedok = siloHost.StartOrleansSilo();
            if (!startedok)
                throw new SystemException($"Failed to start Orleans silo '{siloHost.Name}' as a {siloHost.Type} node");
        }

        static void ShutdownSilo()
        {
            if (siloHost != null)
            {
                siloHost.Dispose();
                GC.SuppressFinalize(siloHost);
                siloHost = null;
            }
        }


    }
}
