using System.Threading.Tasks;
using UrlShortener.Models;

namespace UrlShortener.Repository
{
    public interface IShortenerRepository
    {
        Task<string> AddEntry(TinyUrlDTO entry);
    }
}