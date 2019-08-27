using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestProject.Application.Persons;
using TestProject.Domain.Persons;
using TestProject.Common.DAL.MongoDB;
using TestProject.Common.Entities;
using TestProject.Common.DAL.Core;

namespace TestProject.Module.WebApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;

        public PersonController()
        {
            var context = new InMemoryDbContext<Person, IdInt>();
            _personRepository = new PersonRepository(context);
            _personRepository.CreateAsync(new Person(1) { FirstName = "1", LastName = "2" });
            _personRepository.CreateAsync(new Person(2) { FirstName = "3", LastName = "4" });
            _personRepository.CreateAsync(new Person(3) { FirstName = "5", LastName = "6" });
        }

        [HttpGet]
        public async Task<ActionResult<IList<Person>>> GetAll()
        {
            var personList = await _personRepository.GetListAsync();
            if (personList == null || personList.Count == 0)
                return NotFound();
            return new JsonResult(personList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            var person = await _personRepository.GetAsync(id);
            if (person == null)
                return NotFound();
            return new JsonResult(person);
        }

        // POST api/Person/5
        [HttpPost]
        public async Task<IActionResult> Create(Person person)
        {
            await _personRepository.CreateAsync(person);
            return Ok();
        }

        // PUT api/Person/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Person person)
        {
            await _personRepository.EditAsync(person);
            return Ok();
        }

        // DELETE api/Person/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _personRepository.DeleteAsync(id);
        }
    }
}
