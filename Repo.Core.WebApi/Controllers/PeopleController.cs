using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Repo.Core.Contracts;
using Repo.Core.Storage.Interfaces;

namespace Repo.Core.WebApi.Controllers
{
    [Route("api/people/")]
    public class PeopleController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;

        public PeopleController(IPersonRepository personRepository)
        {
            _personRepository = personRepository ?? throw new System.ArgumentNullException(nameof(personRepository));
        }

        [HttpGet]
        public IEnumerable<Person> GetAll()
        {
            return _personRepository.GetAllPeople();
        }

        [HttpPost, Route("")]
        public IActionResult AddPerson([FromQuery]string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new System.ArgumentNullException(nameof(name));
            }

            _personRepository.AddPerson(new Person()
            {
                Name = name
            });

            return new OkResult();
        }
    }
}
