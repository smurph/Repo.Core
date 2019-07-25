using System;
using MongoDB.Driver;
using Repo.Core.Contracts;
using Repo.Core.Storage.Interfaces;

namespace Repo.Core.Storage.Mongo
{
    public class MongoContext : IMongoContext
    {
        private IMongoClient _mongoClient;
        private readonly MongoConnectionSettings _connectionSettings;

        public MongoContext(IMongoClient mongoClient, MongoConnectionSettings connectionSettings)
        {
            _mongoClient = mongoClient ?? throw new ArgumentNullException(nameof(mongoClient));
            _connectionSettings = connectionSettings ?? throw new ArgumentNullException(nameof(connectionSettings));
        }

        public IMongoCollection<Person> GetPersonCollection()
        {
            return GetDatabase().GetCollection<Person>(_connectionSettings.PersonCollectionName);
        }

        private IMongoDatabase GetDatabase()
        {
            return _mongoClient.GetDatabase(_connectionSettings.DatabaseName);
        }
    }
}
