using System.Threading.Tasks;

namespace PubSubContracts
{
    public interface IEventHandler : Orleans.IGrainObserver
    {
        void Handle(string msg);
    }
}