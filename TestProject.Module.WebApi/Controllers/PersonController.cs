using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestProject.Application.Persons;
using TestProject.Domain.Persons;
using TestProject.Common.DAL.MongoDB;
using TestProject.Common.Entities;

namespace TestProject.Module.WebApi
{
    [ApiController]
    [Produces("application/json")]
    //[ApiVersion("1.0")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]/[action]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;

        public PersonController()
        {
            //_personRepository = personRepository;
            var client = new MongoDB.Driver.MongoClient("mongodb://localhost:27017");
            var context = new MongoDbContext<Person, IdInt>(client, "MongoDbTest", "Person");
            _personRepository = new PersonRepository(context);
        }

        public ActionResult Index()
        {
            return Ok();
        }

        // GET api/Person
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var personList = await _personRepository.GetListAsync();
            if (personList == null || personList.Count == 0)
                return NotFound();
            return new JsonResult(personList);
        }

        // GET api/Person/5
        [HttpGet]
        public async Task<IActionResult> GetSingle(int id)
        {
            var person = await _personRepository.GetAsync(id);
            if (person == null)
                return NotFound();
            return new JsonResult(person);
        }

        // POST api/Person/5
        [HttpPost]
        public async Task<IActionResult> Create(string personJson)
        {
            var person = JsonConvert.DeserializeObject(personJson) as Person;
            if (person == null)
                return BadRequest();
            await _personRepository.CreateAsync(person);
            return Ok();
        }

        // PUT api/Person/5
        [HttpPut]
        public async Task<IActionResult> Edit(string personJson)
        {
            var person = JsonConvert.DeserializeObject(personJson) as Person;
            if (person == null)
                return BadRequest();
            await _personRepository.EditAsync(person);
            return Ok();
        }

        // DELETE api/Person5
        [HttpDelete]
        public async Task Delete(int id)
        {
            await _personRepository.DeleteAsync(id);
        }
    }
}