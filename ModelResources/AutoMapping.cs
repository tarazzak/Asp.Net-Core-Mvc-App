using AutoMapper;

namespace School.Entities.Resources
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Person, PersonModel>(); 
            CreateMap<PersonModel, Person>(); 
            CreateMap<Course, CourseModel>(); 
            CreateMap<CourseInstructor, CourseInstructorModel>(); 
            CreateMap<Department, DepartmentModel>(); 
            CreateMap<OfficeAssignment, OfficeAssignmentModel>(); 
            CreateMap<StudentGrade, StudentGradeModel>(); 
            CreateMap<OnlineCourse, OnlineCourseModel>(); 
            CreateMap<OnsiteCourse, OnsiteCourseModel>(); 
        }
    }
}
