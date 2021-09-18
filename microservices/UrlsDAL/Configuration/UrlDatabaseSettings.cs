namespace UrlsDAL.Configuration
{
    public class UrlDatabaseSettings : IUrlDatabaseSettings
    {
        public string TinyUrlsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}