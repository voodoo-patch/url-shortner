using System.Threading.Tasks;

namespace UrlShortner.Services
{
    public interface IShortnerService
    {
        Task<string> Shorten(string url);
    }
}