using System.Threading.Tasks;

namespace UrlShortner.Services
{
    public interface IKeyGeneratorService
    {
        Task<string> GetKey();
    }
}