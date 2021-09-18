namespace KeysDAL.Configuration
{
    public class KeyDatabaseSettings : IKeyDatabaseSettings
    {
        public string FreshKeysCollectionName { get; set; }
        public string TakenKeysCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}