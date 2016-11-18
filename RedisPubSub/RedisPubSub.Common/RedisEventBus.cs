using System;
using ServiceStack.Redis;
using System.Text;
using System.Threading.Tasks;

namespace RedisPubSub.Common
{
    public class RedisEventBus : IEventBus, IDisposable
    {
        private static readonly Encoding encoding = Encoding.UTF8;
        private readonly IRedisClient _publisher;
        private readonly ISerializer _serializer;

        public RedisEventBus(IRedisClient publisher, ISerializer serializer)
        {
            _publisher = publisher;
            _serializer = serializer;
        }



        public async  Task Publish<T>(T @event) where T : IEvent
        {
            var message = encoding.GetString(await _serializer.SerializeAsync(@event));
            _publisher.PublishMessage(@event.GetType().AssemblyQualifiedName, message);
        }



        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {

                _publisher.Dispose();
            }
        }
    }
}