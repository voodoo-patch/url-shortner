using System.Threading.Tasks;
using UrlShortner.Models;

namespace UrlShortner.Repository
{
    public class ShortnerRepository : IShortnerRepository
    {
        public Task<string> AddEntry(TinyUrlDTO entry)
        {
            throw new System.NotImplementedException();
        }
    }
}