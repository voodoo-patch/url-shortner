namespace KeyGeneratorService.Models
{
    public class StatsDTO
    {
        public ulong Generated { get; set; }
        public ulong Collisions { get; set; }
        public ulong Valid => this.Generated - this.Collisions;
    }
}