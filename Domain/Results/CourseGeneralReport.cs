using Domain.Entities;

namespace Domain.Results
{
    public class CourseGeneralReport
    {
        public string CourseName { get; set; }
        public string Lecturer { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public int Average { get; set; }
    }
}