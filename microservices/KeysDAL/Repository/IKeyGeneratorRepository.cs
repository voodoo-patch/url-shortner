using System.Threading.Tasks;
using KeysDAL.Entities;

namespace KeysDAL.Repository
{
    public interface IKeyGeneratorRepository
    {
        Task<FreshKey> GetFreshKey();
    }
}