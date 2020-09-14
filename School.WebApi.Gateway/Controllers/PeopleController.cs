using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using School.Entities;
using School.Services.People.Interfaces;
using Microsoft.Extensions.Logging;
using School.Entities.Resources;

namespace School.Microservices.People.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly ILogger<PeopleController> _logger;
        private readonly IMapper _mapper;
        public PeopleController(IPersonService personService, IMapper mapper, ILogger<PeopleController> logger)
        {
            _personService = personService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> Get()
        {
            var result = await _personService.GetAllAsync().ConfigureAwait(false);

            if (result == null) return NotFound();

            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> Get(int id)
        {
            var person = await _personService.FindFirstByCriteriaAsync(p => p.PersonId == id).ConfigureAwait(false);

            if (person == null) return NotFound();

            return person;
        }

        [HttpGet]
        [Route("GetPersonModelAsync/{id}")]
        public async Task<PersonModel> GetPersonModelAsync(int id)
        {
            var person = await _personService.FindFirstByCriteriaAsync(p => p.PersonId == id).ConfigureAwait(false);

            return _mapper.Map<Person, PersonModel>(person);
        }

        
        [HttpDelete("{id}")]
        [Route("Delete/{id}")]
        public async Task<ActionResult<Person>> Delete(int id)
        {
            var result = await _personService.FindFirstByCriteriaAsync(p => p.PersonId == id).ConfigureAwait(false); 

            if (result == null) return NotFound();

            var rowsAffected = await _personService.RemoveAsync(result).ConfigureAwait(false);
            
            return rowsAffected == 1 ? Ok() : StatusCode(100001); //Unexpected result
        }

        [HttpPost]
        public async Task<ActionResult<Person>> Post(Person person)
        {
            await _personService.AddAsync(person).ConfigureAwait(false);

            return CreatedAtAction("Get", new { id = person.PersonId }, person);
        }
    }
}
