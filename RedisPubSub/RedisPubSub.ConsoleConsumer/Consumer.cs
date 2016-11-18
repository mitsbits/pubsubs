using System;
using System.Text;
using System.Threading.Tasks;
using RedisPubSub.Common;
using ServiceStack.Redis;

namespace RedisPubSub.ConsoleConsumer
{
    public class Consumer : RedisSubscriber, IHandlesMessage<HelloWorldEvent>
    {
        private static readonly Encoding encoding = Encoding.UTF8;
        private readonly ISerializer _serializer;
        public Consumer(IRedisClient consumer,  ISerializer serializer) : base(consumer, new [] {typeof(HelloWorldEvent).AssemblyQualifiedName})
        {
            _serializer = serializer;
        }

        protected override void OnMessage(string channel, string message)
        {
            var evt = _serializer.Deserialize<HelloWorldEvent>(encoding.GetBytes(message));
            Handle(evt).ConfigureAwait(false);
        }

        public Task Handle(HelloWorldEvent message)
        {
            Console.WriteLine($"Greeting: {message.Greeting}");
            return Task.CompletedTask;
        }
    }
}
