using System;
using System.Linq;
using System.Transactions;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using School.Entities;
using School.Services.Interfaces;
using School.Services.People.Implementations;

namespace School.NUnitTests
{
    [TestFixture]
    public class SchoolPersonServiceTest
    {
        private ISchoolService<Person> _schoolService;
        //Transaction will be rolled back after each test.
        private TransactionScope _trans;

        private int personTestId = 1;
        private string personTestLastName = "test";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _trans = new TransactionScope();
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            _trans.Dispose();
        }

        [OnError]
        public void OnError()
        {
            if (TestContext.CurrentContext.Result.FailCount > 0)
            {
                Console.WriteLine(TestContext.CurrentContext.Result.Message);
            }
        }
        
        [Test]
        public void remove_person_with_sp()
        {
            //Arrange 
            var personService = new PersonService();
          
            var person = personService.FindByCriteria(p => p.PersonId == personTestId, false, true).FirstOrDefault();

            Assert.IsTrue(person != null);

            //Act
            var rowsAffected = personService.DeletePerson(person);
            person = personService.FindByCriteria(p => p.PersonId == personTestId, false, true).FirstOrDefault();

            //Assert (Only 1 person instant deleted?)
            Assert.IsTrue(person == null && rowsAffected == 1);
        }

        [Test]
        public void remove_person()
        {
            //Arrange 
            _schoolService = new PersonService();
            var person = _schoolService.FindByCriteria(p => p.PersonId == 1, false, true).FirstOrDefault();

            Assert.IsTrue(person != null);

            //Act
            person = _schoolService.FindByCriteria(p => p.PersonId == 1, false, true).FirstOrDefault();

            //Assert
            Assert.IsTrue(person == null);
        }

        [Test]
        public void get_person_all()
        {
            //Arrange and Act
            _schoolService = new PersonService();
            var people = _schoolService.GetAll(false);
           
            //Assert
            Assert.IsTrue(people.Any());
        }

        [Test]
        public void get_person_by_id()
        {
            //Arrange and Act
            _schoolService = new PersonService();
            var people = _schoolService.FindByCriteria(person => person.PersonId == personTestId);

            //Assert
            Assert.IsTrue(people.Count() == 1);
        }

        [Test]
        public void update_person()
        {
            //Arrange 
            _schoolService = new PersonService();
            var person = _schoolService.GetAll().First();

            //Act
            person.LastName = personTestLastName;
            var updatedPerson = _schoolService.Update(person);
            
            //Assert
            Assert.IsTrue(updatedPerson.LastName == personTestLastName);
        }
    }
}