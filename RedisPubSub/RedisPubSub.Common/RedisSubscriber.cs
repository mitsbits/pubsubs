using ServiceStack;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RedisPubSub.Common
{
    public abstract class RedisSubscriber : IDisposable
    {
        private readonly IRedisClient _consumer;
        private readonly IRedisSubscription _subscription;

        protected RedisSubscriber(IRedisClient consumer, IEnumerable<string> channels)
        {
            _consumer = consumer;
            Channels = channels;
            _subscription = _consumer.CreateSubscription();
  

            _subscription.OnSubscribe = OnSubscribe;
            _subscription.OnUnSubscribe = OnUnSubscribe;
            _subscription.OnMessage = OnMessage;
        }


        public void Listen()
        {
            _subscription.SubscribeToChannels(Channels.ToArray());
        }

        protected virtual void OnSubscribe(string channel)
        {
            Console.WriteLine($"Subscribed to {channel}");
        }

        protected virtual void OnUnSubscribe(string channel)
        {
            Console.WriteLine($"Unsubscribed from {channel}");
        }

        protected abstract void OnMessage(string channel, string message);

        private IEnumerable<string> Channels { get; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _subscription.UnSubscribeFromAllChannels();
                _subscription.Dispose();
                _consumer.Dispose();
            }
        }
    }
}