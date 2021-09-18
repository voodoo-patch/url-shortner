using System.Threading.Tasks;

namespace UrlShortener.Services
{
    public interface IKeyGeneratorService
    {
        Task<string> GetKey();
    }
}