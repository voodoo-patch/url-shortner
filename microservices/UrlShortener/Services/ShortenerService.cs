using System;
using System.Threading.Tasks;
using UrlsDAL.Entities;
using UrlsDAL.Repository;
using UrlShortener.Models;

namespace UrlShortener.Services
{
    public class ShortenerService : IShortenerService
    {
        private readonly IKeyGeneratorService keyGeneratorService;
        private readonly IShortenerRepository shortenerRepository;

        public ShortenerService(IKeyGeneratorService keyGeneratorService, IShortenerRepository shortenerRepository)
        {
            this.keyGeneratorService = keyGeneratorService;
            this.shortenerRepository = shortenerRepository;
        }

        public async Task<string> Shorten(string url)
        {
            string key = await this.keyGeneratorService.GetKey();
            if (key is null || string.IsNullOrWhiteSpace(key))
            {
                throw new ApplicationException("Fresh key unavailable");
            }

            var entry = new TinyUrl()
            {
                Id = key,
                OriginalUrl = url,
                ShortUrl = key,
                ExpiresOn = null,
                UserId = null
            };
            try
            {
                await this.shortenerRepository.AddEntry(entry);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Unable to persist the current configuration with key '{key}'.", ex);
            }

            return key;
        }

        public async Task<TinyUrlDTO> GetUrl(string shortenedUrl)
        {
            TinyUrlDTO result = null;
            var tinyUrl = await this.shortenerRepository.GetUrl(shortenedUrl);

            if (tinyUrl != null)
            {
                result = new TinyUrlDTO()
                {
                    Id = tinyUrl.Id,
                    OriginalUrl = tinyUrl.OriginalUrl,
                    ShortUrl = tinyUrl.ShortUrl,
                    UserId = tinyUrl.UserId,
                };
            }

            return result;
        }
    }
}