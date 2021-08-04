using System.Collections.Generic;
using Domain.Entities;
using Domain.Utils;
using Domain.Interfaces.Services;
using Domain.Interfaces.Repositories;
using log4net;
using System.Linq;

namespace Service
{
    public sealed class CourseService : BaseService<Course>, ICourseService
    {
        public CourseService(ILog logger, ICourseRepository repository) : base(logger, repository)
        {

        }

        public override Result Insert(IEnumerable<Course> instances)
        {
            var result = new Result();

            if (instances == null || instances.Count() == 0)
            {
                return result;
            }

            foreach (var course in instances)
            {
                if (course.Id <= 0)
                {
                    result.AddError("Course Id must be major than zero (0).");
                    return result;
                }

                if (string.IsNullOrEmpty(course.Lecturer))
                {
                    result.AddError("Lecturer is null or empty, verify.");
                    return result;
                }

                if (string.IsNullOrEmpty(course.Name))
                {
                    result.AddError("Name is null ou empty, verify.");
                    return result;
                }

                if (course.MaxNumberOfStudents <= 0)
                {
                    result.AddError("Max number of students must be major then zero (0).");
                    return result;
                }
            }

            return this._repository.Insert(instances);
        }
        public override Result Update(IEnumerable<Course> instances)
        {
            var result = new Result();

            result.AddError("Update(courses) method not implemented yet.");

            return result;
        }
        public override Result Delete(IEnumerable<Course> instances)
        {
            var result = new Result();

            result.AddError("Delete(courses) method not implemented yet.");

            return result;
        }

        public Result<Course> GetById(int id)
        {
            return (this._repository as ICourseRepository).GetById(id);
        }

        public Result<IEnumerable<Course>> GetAll()
        {
            return (this._repository as ICourseRepository).GetAll();
        }
    }
}