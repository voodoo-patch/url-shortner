using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using UrlShortner.Models;
using UrlShortner.Repository;

namespace UrlShortner.Services
{
    public class ShortnerService : IShortnerService
    {
        private readonly IKeyGeneratorService keyGeneratorService;
        private readonly IShortnerRepository shortnerRepository;

        public ShortnerService(IKeyGeneratorService keyGeneratorService, IShortnerRepository shortnerRepository)
        {
            this.keyGeneratorService = keyGeneratorService;
            this.shortnerRepository = shortnerRepository;
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
                await this.shortnerRepository.AddEntry(entry);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Unable to persist the current configuration with key '{key}'.", ex);
            }

            return key;
        }
    }
}