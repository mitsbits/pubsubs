using NServiceBus;
using System;

namespace Shared
{
    public class PlaceOrder : ICommand
    {
        public Guid Id { get; set; }
        public string Product { get; set; }
    }

    public class OrderPlaced :
    IEvent
    {
        public Guid OrderId { get; set; }
    }
}