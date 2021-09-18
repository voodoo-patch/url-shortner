using System.Threading.Tasks;
using UrlShortener.Entities;
using UrlShortener.Models;

namespace UrlShortener.Repository
{
    public interface IShortenerRepository
    {
        Task<TinyUrl> AddEntry(TinyUrl entry);
        Task<TinyUrl> GetUrl(string shortenedUrl);
    }
}