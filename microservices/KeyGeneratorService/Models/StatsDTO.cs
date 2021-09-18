namespace KeyGeneratorService.Models
{
    public class StatsDTO
    {
        public long Generated { get; set; }
        public long Collisions { get; set; }
        public long Valid => this.Generated - this.Collisions;
    }
}