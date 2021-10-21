using System.Threading.Tasks;

namespace ProducerApi.Providers
{
    public interface IProducerService
    {
         Task SendAsync(string data);
    }
}