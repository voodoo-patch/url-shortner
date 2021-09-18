using KeyGeneratorService.Models;

namespace KeyGeneratorService.Services
{
    public sealed class StatsSingletonService
    {
        private static readonly StatsSingletonService instance = new StatsSingletonService();
        private static StatsDTO stats;
        static object padlock = new object();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static StatsSingletonService()
        {
        }

        private StatsSingletonService()
        {
            stats = new StatsDTO();
        }

        public static StatsDTO StatsInstance => stats;

        public static long IncreaseCounter()
        {
            lock (padlock)
            {
                return ++stats.Generated;
            }
        }

        public static long IncreaseCollisionCounter()
        {
            lock (padlock)
            {
                return ++stats.Collisions;
            }
        }
    }
}
