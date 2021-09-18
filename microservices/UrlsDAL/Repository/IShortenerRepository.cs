using System.Threading.Tasks;
using UrlsDAL.Entities;

namespace UrlsDAL.Repository
{
    public interface IShortenerRepository
    {
        Task<TinyUrl> AddEntry(TinyUrl entry);
        Task<TinyUrl> GetUrl(string shortenedUrl);
    }
}