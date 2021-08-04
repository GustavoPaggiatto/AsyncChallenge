using System;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces.Adapters;
using Domain.Results;
using Domain.Utils;

namespace Adapter
{
    public class CourseReportAdapter : IAdapter<CourseGeneralReport>
    {
        public Result<CourseGeneralReport> Adaptee(params object[] parameters)
        {
            var result = new Result<CourseGeneralReport>();

            if (parameters == null || parameters.Length == 0)
                return result;

            var course = parameters[0] as Course;

            result.Content = new CourseGeneralReport();
            result.Content.Lecturer = course.Lecturer;
            result.Content.CourseName = course.Name;
            result.Content.Average = (int)(course.Students.Sum(s => DateTime.Now.Subtract(s.BirthDate).Days / 365) / course.Students.Count());

            var minDate = course.Students.Min(s => s.BirthDate);
            var maxDate = course.Students.Max(s => s.BirthDate);

            result.Content.MaxAge = DateTime.Now.Subtract(minDate).Days / 365;
            result.Content.MinAge = DateTime.Now.Subtract(maxDate).Days / 365;

            return result;
        }
    }
}
