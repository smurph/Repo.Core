using System.Collections.Generic;
using Repo.Core.Contracts;

namespace Repo.Core.Storage.Interfaces
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetAllPeople();

        void AddPerson(Person person);

        Person FindByName(string name);
    }
}
