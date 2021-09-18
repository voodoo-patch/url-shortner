using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace KeyGeneratorService.Services
{

    public class KeygenService : BackgroundService
    {
        private readonly ILogger<KeygenService> _logger;
        private const int INTERVAL = 1;
        // in order to decrease collisions, `_` and `-` characters could be added to the dictionary
        private const string DICTIONARY = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        private const int KEY_LENGTH = 8;
        private const int TRIALS = 1000000;

        HashSet<string> keys = new HashSet<string>();
        public KeygenService(ILogger<KeygenService> logger)
        {
            this._logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug($"KeygenService is starting.");

            stoppingToken.Register(() =>
                _logger.LogDebug($"KeygenService background task is stopping."));
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
                while (!stoppingToken.IsCancellationRequested && StatsSingletonService.Generated <= TRIALS)
                {
                    _logger.LogDebug($"KeygenService task doing background work.");

                    string key = this.GenerateKey(rngCsp);

                    bool valid = true;
                    // save key into db if not present

                    // to replace with db save
                    valid = keys.Add(key);

                    if (valid)
                    {
                        StatsSingletonService.IncreaseCounter();
                    }
                    else
                    {
                        StatsSingletonService.IncreaseCollisionCounter();
                    }

                    // used to guarantee web host to respond to stats calls
                    await Task.Delay(INTERVAL);
                }

            _logger.LogDebug($"KeygenService background task is stopping.");
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