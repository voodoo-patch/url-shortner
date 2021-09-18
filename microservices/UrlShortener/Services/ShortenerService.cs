using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using UrlShortener.Models;
using UrlShortener.Repository;

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
            var entry = new TinyUrlDTO()
            {
                Id = key,
                OriginalUrl = url,
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
    }
}