using System.Threading.Tasks;
using UrlShortener.Models;

namespace UrlShortener.Repository
{
    public class ShortenerRepository : IShortenerRepository
    {
        public Task<string> AddEntry(TinyUrlDTO entry)
        {
            throw new System.NotImplementedException();
        }
    }
}