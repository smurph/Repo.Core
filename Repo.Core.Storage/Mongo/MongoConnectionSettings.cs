namespace Repo.Core.Storage.Mongo
{
    public class MongoConnectionSettings
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public string PersonCollectionName { get; set; }
    }
}
