


namespace RedisPubSub.Common
{
    public class HelloWorldEvent :IEvent
    {
        private readonly string _greeting;

        public HelloWorldEvent(string greeting)
        {
            _greeting = greeting;
        }

        public string Greeting => $"Hello world, this is {_greeting}";
    }
}
