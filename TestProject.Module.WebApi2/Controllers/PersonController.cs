using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestProject.Application.Persons;
using TestProject.Domain.Persons;

namespace TestProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonRepository _personRepository;

        public PersonController(ILogger<PersonController> logger, IPersonRepository personRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _personRepository = personRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation(nameof(GetAll));
            var personList = await _personRepository.GetListAsync();
            if (personList == null || personList.Count == 0)
            {
                _logger.LogWarning($"{nameof(GetAll)} - нет результатов");
                return NotFound();
            }
            return new JsonResult(personList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            _logger.LogInformation(nameof(GetSingle));
            var person = await _personRepository.GetAsync(id);
            if (person == null)
            {
                _logger.LogWarning($"{nameof(GetSingle)} - {id} - нет результатов");
                return NotFound();
            }
            return new JsonResult(person);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Person person)
        {
            _logger.LogInformation(nameof(Create));
            await _personRepository.CreateAsync(person);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Person person)
        {
            _logger.LogInformation(nameof(Edit));
            await _personRepository.EditAsync(person);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            _logger.LogInformation(nameof(Delete));
            await _personRepository.DeleteAsync(id);
        }
    }
}