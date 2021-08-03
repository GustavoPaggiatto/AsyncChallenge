using System.Collections.Generic;
using Domain.Entities;
using Domain.Utils;
using Domain.Interfaces.Repositories;
using log4net;
using System;
using System.Linq;

namespace Data
{
    public sealed class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        private static List<Course> _courses;

        public CourseRepository(ILog logger) : base(logger)
        {
        }

        static CourseRepository()
        {
            _courses = new List<Course>();
        }

        public override Result Insert(IEnumerable<Course> instances)
        {
            var result = new Result();

            try
            {
                _courses.AddRange(instances);
            }
            catch (Exception ex)
            {
                this._logger.Error(ex);
                result.AddError("There was an error when registering the course, try again or contact the responsible team.");
            }

            return result;
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
            var result = new Result<Course>();

            try
            {
                result.Content = _courses.FirstOrDefault(c => c.Id == id);
            }
            catch (Exception ex)
            {
                this._logger.Error(ex);
                result.AddError("An error occurred while retrieving course data, try again or contact the responsible team.");
            }

            return result;
        }
    }
}