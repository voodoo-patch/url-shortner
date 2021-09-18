using System.Threading.Tasks;
using MongoDB.Driver;
using UrlShortener.Configuration;
using UrlShortener.Entities;

namespace UrlShortener.Repository
{
    public class KeyGeneratorRepository : IKeyGeneratorRepository
    {
        private readonly IMongoCollection<FreshKey> freshKeys;
        private readonly IMongoCollection<TakenKey> takenKeys;

        public KeyGeneratorRepository(IKeyDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            this.freshKeys = database.GetCollection<FreshKey>(settings.FreshKeysCollectionName);
            this.takenKeys = database.GetCollection<TakenKey>(settings.TakenKeysCollectionName);
        }

        public async Task<FreshKey> GetFreshKey()
        {
            var freshKey = await this.freshKeys.FindOneAndDeleteAsync(key => true);
            if (freshKey != null)
            {
                var takenKey = new TakenKey()
                {
                    Key = freshKey.Key
                };
                await this.takenKeys.InsertOneAsync(takenKey);
            }

            return freshKey;
        }
    }
}