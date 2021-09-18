namespace KeysDAL.Configuration
{
    public interface IKeyDatabaseSettings
    {
        string FreshKeysCollectionName { get; set; }
        string TakenKeysCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}