using System.Linq;
using System.Threading.Tasks;
using UrlsDAL.Configuration;
using MongoDB.Driver;
using UrlsDAL.Entities;

namespace UrlsDAL.Repository
{
    public class ShortenerRepository : IShortenerRepository
    {
        private readonly IMongoCollection<TinyUrl> tinyUrls;

        public ShortenerRepository(IUrlDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            this.tinyUrls = database.GetCollection<TinyUrl>(settings.TinyUrlsCollectionName);
        }

        public async Task<TinyUrl> AddEntry(TinyUrl entry)
        {
            await tinyUrls.InsertOneAsync(entry);
            return entry;
        }

        public async Task<TinyUrl> GetUrl(string shortenedUrl)
        {
            var tinyUrl = await tinyUrls.Find<TinyUrl>(url => url.ShortUrl == shortenedUrl)
                .FirstOrDefaultAsync();

            return tinyUrl;
        }
    }
}