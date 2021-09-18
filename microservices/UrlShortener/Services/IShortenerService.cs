using System.Threading.Tasks;
using UrlShortener.Models;

namespace UrlShortener.Services
{
    public interface IShortenerService
    {
        Task<string> Shorten(string url);
        Task<TinyUrlDTO> GetUrl(string shortenedUrl);
    }
}