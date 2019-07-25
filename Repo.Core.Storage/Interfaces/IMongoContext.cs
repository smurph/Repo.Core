using MongoDB.Driver;
using Repo.Core.Contracts;

namespace Repo.Core.Storage.Interfaces
{
    public interface IMongoContext
    {
        IMongoCollection<Person> GetPersonCollection();
    }
}
