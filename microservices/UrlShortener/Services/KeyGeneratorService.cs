using System.Threading.Tasks;
using KeysDAL.Repository;

namespace UrlShortener.Services
{
    public class KeyGeneratorService : IKeyGeneratorService
    {
        private readonly IKeyGeneratorRepository keyGeneratorRepository;

        public KeyGeneratorService(IKeyGeneratorRepository keyGeneratorRepository)
        {
            this.keyGeneratorRepository = keyGeneratorRepository;
        }

        public async Task<string> GetKey()
        {
            var freshKey = await this.keyGeneratorRepository.GetFreshKey();
            
            return freshKey?.Key;
        }
    }
}