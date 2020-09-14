//using System;
//using System.Linq;
//using System.Linq.Expressions;
//using Microsoft.Data.SqlClient;
//using Microsoft.EntityFrameworkCore;
//using School.Entities;
//using School.Services.Interfaces;

//namespace School.Services.Implementations
//{
//    public class PersonService : SchoolService<Person>, IPersonService
//    {
//        public override IQueryable<Person> FindByCriteria(Expression<Func<Person, bool>> expression, bool asNoTracking = true, bool includeChildren = false)
//        {
//            var entities = asNoTracking ? Entities.Where(expression).AsNoTracking() : Entities.Where(expression);
//            if (includeChildren)
//                return entities
//                    .Include(person => person.CourseInstructors)
//                    .Include(person => person.OfficeAssignment);
//            return entities;
//        }

//        public override IQueryable<Person> GetAll(bool asNoTracking = true, bool includeChildren = false)
//        {
//            var entities = asNoTracking ? Entities.AsNoTracking() : Entities;
//            if (includeChildren)
//            {
//                return entities
//                    .Include(person => person.CourseInstructors)
//                    .Include(person => person.OfficeAssignment);
//            }

//            return entities;
//        }

//        public int DeletePerson(Person person)
//        {
//            var personIdParam = new SqlParameter()
//            {
//                ParameterName = "@PersonId",
//                Value = person.PersonId
//            };

//            return base.ExecuteSqlCommand("dbo.DeletePerson @PersonId", new object[] { personIdParam });
//        }
//    }
//}