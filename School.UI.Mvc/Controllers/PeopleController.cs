using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using School.Entities;
using School.Entities.Resources;
using School.Services.People.Interfaces;
using School.Services.People.Implementations;
using AutoMapper;

namespace School.UI.Mvc.Controllers
{
    public class PeopleController : Controller
    {
        private IPersonService _personService;
        private readonly IMapper _mapper;

        public PeopleController(IPersonService personService, IMapper mapper)
        {
            _personService = personService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var people = _personService.GetAll().ToList();

            //ToDo: Move to factory service
            var peopleModel = _mapper.Map<List<Person>, List<PersonModel>>(people);

            return View(peopleModel);
        }

        public IActionResult Edit(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = _personService.FindByCriteria(p => p.PersonId == id).FirstOrDefault();

            //ToDo: Move to factory service
            var personModel = _mapper.Map<Person, PersonModel>(person);

            return View(personModel);
        }

        public IActionResult Update([FromForm] PersonModel personModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //ToDo: Move to factory service
            var person = _mapper.Map<PersonModel, Person>(personModel);

            _personService.Update(person);

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //ToDo: Move to factory service
            var person = _personService.FindByCriteria(p => p.PersonId == id).FirstOrDefault();
            var personModel = _mapper.Map<Person, PersonModel>(person);

            return View(personModel);
        }

        public IActionResult Delete(int id)
        {
            //ToDo: provide implementation 
            throw new NotImplementedException();
        }

        public async Task<IActionResult> Create()
        {
            //ToDo: provide implementation 
            throw new NotImplementedException();
        }
    }
}
