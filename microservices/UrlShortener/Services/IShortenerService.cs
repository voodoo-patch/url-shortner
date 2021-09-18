using System.Threading.Tasks;

namespace UrlShortener.Services
{
    public interface IShortenerService
    {
        Task<string> Shorten(string url);
    }
}