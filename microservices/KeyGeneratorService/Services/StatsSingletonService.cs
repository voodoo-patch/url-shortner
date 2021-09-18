using KeyGeneratorService.Models;

namespace KeyGeneratorService.Services
{
    public sealed class StatsSingletonService
    {
        private static readonly StatsSingletonService instance = new StatsSingletonService();
        private static ulong generated { get; set; }
        private static ulong collisions { get; set; }
        static object padlock = new object();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static StatsSingletonService()
        {
        }

        private StatsSingletonService()
        {
            generated = 0;
            collisions = 0;
        }

        public static ulong Generated
        {
            get
            {
                lock (padlock)
                {
                    return generated;
                }
            }
        }

        public static ulong Collisions
        {
            get
            {
                lock (padlock)
                {
                    return collisions;
                }
            }
        }

        public static ulong IncreaseCounter()
        {
            lock (padlock)
            {
                return ++generated;
            }
        }

        public static ulong IncreaseCollisionCounter()
        {
            lock (padlock)
            {
                return ++collisions;
            }
        }
    }
}