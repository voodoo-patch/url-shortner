namespace UrlsDAL.Configuration
{
    public interface IUrlDatabaseSettings
    {
        string TinyUrlsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        
    }
}