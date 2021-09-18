using System.Threading.Tasks;
using KeysDAL.Entities;

namespace KeysDAL.Repository
{
    public interface IKeyGeneratorRepository
    {
        Task AddFreshKey(FreshKey key);
        Task<FreshKey> GetFreshKey();
    }
}