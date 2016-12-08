using System.Threading.Tasks;
using Orleans;

namespace PubSubContracts
{
    public interface IHello : Orleans.IGrainWithIntegerKey
    {
        Task<string> SayHello(string msg);
    }
}
