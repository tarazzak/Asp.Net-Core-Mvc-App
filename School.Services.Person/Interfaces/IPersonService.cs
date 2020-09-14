using School.Entities;
using School.Services.Interfaces;

namespace School.Services.People.Interfaces
{
    public interface IPersonService: ISchoolService<Person>
    {
        int DeletePerson(Person person);
    }
}