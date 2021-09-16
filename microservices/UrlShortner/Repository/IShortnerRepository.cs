using System.Threading.Tasks;
using UrlShortner.Models;

namespace UrlShortner.Repository
{
    public interface IShortnerRepository
    {
        Task<string> AddEntry(TinyUrlDTO entry);
    }
}