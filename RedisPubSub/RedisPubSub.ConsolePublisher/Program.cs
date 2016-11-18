using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using RedisPubSub.Common;
using ServiceStack.Redis;

namespace RedisPubSub.ConsolePublisher
{
    public class Program
    {
        private static int _runs = 0;
        public static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.Register<IRedisClient>(c => new RedisClient("127.0.0.1", 6379)).InstancePerLifetimeScope();
            builder.RegisterType<RedisEventBus>().As<IEventBus>().InstancePerLifetimeScope();
            builder.RegisterType<NewtonsoftSerializer>().As<ISerializer>().InstancePerLifetimeScope();

            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var tasks = new List<Task>();
                while (_runs < 120)
                {
                    _runs++;
                    Console.WriteLine($"Run {_runs}");

                    var bus = scope.Resolve<IEventBus>();

                    var t = Task.Run(async () =>
                     {
                         var rnd = new Random();
                         var factor = rnd.Next(1, 500);
                         Thread.Sleep(factor * 50);
                         var evt = new HelloWorldEvent($"factor {factor}");
                         Console.WriteLine(evt.Greeting);
           
                         await bus.Publish(evt);
                     });
                    
                    tasks.Add(t);
                }

            }
            Console.WriteLine("Messages sent");
            Console.ReadLine();

        }
    }
}
