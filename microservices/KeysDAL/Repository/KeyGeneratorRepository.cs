using System;
using System.Data;
using System.Threading.Tasks;
using MongoDB.Driver;
using KeysDAL.Configuration;
using KeysDAL.Entities;
using MongoDB.Bson;

namespace KeysDAL.Repository
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

        public async Task AddFreshKey(FreshKey key)
        {
            try
            {
                if (await IsKeyAlreadyTaken(key))
                {
                    throw new DuplicateNameException("Key already taken");
                }
                await this.freshKeys.InsertOneAsync(key);
            }
            catch (Exception ex)
            {
                throw new DuplicateNameException();
            }
        }
        private async Task<bool> IsKeyAlreadyTaken(FreshKey key)
        {
            try
            {
                var taken = await this.takenKeys.FindAsync(k => k.Key == key.Key);
                return taken.Any();
            }
            catch (Exception ex)
            {
                throw new DuplicateNameException();
            }
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

        public async Task<long> CountFreshKeys()
        {
            return await this.freshKeys.CountDocumentsAsync(new BsonDocument());
        }
    }
}