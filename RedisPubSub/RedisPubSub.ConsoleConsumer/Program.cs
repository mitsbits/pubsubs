using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RedisPubSub.Common;
using ServiceStack.Redis;

namespace RedisPubSub.ConsoleConsumer
{
    public class Program
    {
        private static int _messageCounter = 100;
        public static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.Register<IRedisClient>(c => new RedisClient("127.0.0.1", 6379)).InstancePerLifetimeScope();
            builder.RegisterType<NewtonsoftSerializer>().As<ISerializer>().InstancePerLifetimeScope();
            builder.RegisterType<Consumer>().InstancePerLifetimeScope();

            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                var consumer = scope.Resolve<Consumer>();

                consumer.Listen();

            }
        }
    }
}
