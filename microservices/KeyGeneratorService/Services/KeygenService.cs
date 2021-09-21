using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using KeysDAL.Entities;
using KeysDAL.Repository;
using Microsoft.Extensions.Logging;

namespace KeyGeneratorService.Services
{
    public class KeygenService : BackgroundService
    {
        private readonly ILogger<KeygenService> _logger;

        private const int CREATION_INTERVAL = 1;
        private const int QUERY_INTERVAL = 5000;

        // in order to decrease collisions, `_` and `-` characters could be added to the dictionary
        private const string DICTIONARY = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        private const int KEY_LENGTH = 8;
        private const int TRIALS = 1000000;
        private const int BACKUP_KEYS = 100;

        HashSet<string> keys = new HashSet<string>();
        private readonly IKeyGeneratorRepository keyGeneratorRepository;

        public KeygenService(ILogger<KeygenService> logger, IKeyGeneratorRepository keyGeneratorRepository)
        {
            this._logger = logger;
            this.keyGeneratorRepository = keyGeneratorRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"KeygenService is starting.");

            stoppingToken.Register(() =>
                _logger.LogInformation($"KeygenService background task is stopping."));
            await this.DoWork(stoppingToken);

            _logger.LogInformation($"KeygenService background task is stopping.");
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            long actualKeys = 0;
            try
            {
                using RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
                while (!stoppingToken.IsCancellationRequested)
                {
                    // making sure that there are at least 100 fresh keys ready to be used
                    while ((actualKeys = await this.keyGeneratorRepository.CountFreshKeys()) < BACKUP_KEYS)
                    {
                        string key = this.GenerateKey(rngCsp);

                        // save key into db if not present
                        try
                        {
                            var freshKey = new FreshKey()
                            {
                                Key = key
                            };
                            await this.keyGeneratorRepository.AddFreshKey(freshKey);
                            StatsSingletonService.IncreaseCounter();
                        }
                        catch (DuplicateNameException ex)
                        {
                            // Console.WriteLine("Exception occurred \n{0}", JsonSerializer.Serialize(ex));
                            _logger.LogError(ex, "Keygen collision occurred");
                            StatsSingletonService.IncreaseCollisionCounter();
                        }

                        // used to guarantee web host to respond to stats calls
                        await Task.Delay(CREATION_INTERVAL);
                    }

                    await Task.Delay(QUERY_INTERVAL);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "unhandled ex in KGS");
            }
        }

        private string GenerateKey(RNGCryptoServiceProvider rngCsp)
        {
            int size = KEY_LENGTH;
            // Create a byte array to hold the random value.
            byte[] data = new byte[KEY_LENGTH];
            // Fill the array with a random value.
            rngCsp.GetNonZeroBytes(data);

            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(DICTIONARY[b % DICTIONARY.Length]);
            }

            return result.ToString();
        }
    }
}