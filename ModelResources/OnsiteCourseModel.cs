using System;

namespace School.Entities.Resources
{
    public class OnsiteCourseModel
    {
        public int CourseId { get; set; }
        public string Location { get; set; }
        public string Days { get; set; }
        public DateTime Time { get; set; }
    }
}