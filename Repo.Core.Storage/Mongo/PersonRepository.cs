using System.Collections.Generic;
using MongoDB.Driver;
using Repo.Core.Contracts;
using Repo.Core.Storage.Interfaces;

namespace Repo.Core.Storage.Mongo
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IMongoContext _mongoContext;

        public PersonRepository(IMongoContext mongoContext)
        {
            _mongoContext = mongoContext ?? throw new System.ArgumentNullException(nameof(mongoContext));
        }

        public void AddPerson(Person person)
        {
            GetCollection().InsertOne(person);
        }

        public Person FindByName(string name)
        {
            return GetCollection().Find(f => f.Name.Trim().ToLower() == name.Trim().ToLower()).FirstOrDefault();
        }

        public IEnumerable<Person> GetAllPeople()
        {
            return GetCollection().Find(Builders<Person>.Filter.Empty).ToEnumerable();
        }

        private IMongoCollection<Person> GetCollection()
        {
            return _mongoContext.GetPersonCollection();
        }
    }
}
