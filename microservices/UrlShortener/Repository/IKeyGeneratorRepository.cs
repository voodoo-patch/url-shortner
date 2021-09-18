using System.Threading.Tasks;
using UrlShortener.Entities;

namespace UrlShortener.Repository
{
    public interface IKeyGeneratorRepository
    {
        Task<FreshKey> GetFreshKey();
    }
}