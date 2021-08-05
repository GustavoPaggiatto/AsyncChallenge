using System.Collections.Generic;
using Domain.Entities;
using Domain.Utils;
using Domain.Interfaces.Repositories;
using log4net;
using System;
using System.Linq;
using System.Collections;

namespace Data
{
    public sealed class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        private static List<Course> _courses;
        private static object _lock;

        public CourseRepository(ILog logger) : base(logger)
        {
        }

        static CourseRepository()
        {
            _courses = new List<Course>();
            _lock = new object();
        }

        public override Result Insert(IEnumerable<Course> instances)
        {
            var result = new Result();

            try
            {
                lock (_lock)
                {
                    _courses.AddRange(instances);
                }
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
                lock (_lock)
                {
                    var content = _courses.FirstOrDefault(c => c.Id == id);

                    if (content != null)
                        result.Content = content.Clone() as Course;
                }
            }
            catch (Exception ex)
            {
                this._logger.Error(ex);
                result.AddError("An error occurred while retrieving course data, try again or contact the responsible team.");
            }

            return result;
        }

        public Result<int> GetLogins(int courseId)
        {
            var result = new Result<int>();

            try
            {
                lock (_lock)
                {
                    var course = _courses.FirstOrDefault(c => c.Id == courseId);

                    if (course != null)
                        result.Content = course.Students.Count();
                }
            }
            catch (Exception ex)
            {
                this._logger.Error(ex);
                result.AddError("An error occurred while retrieving course logins quantity, " +
                "try again or contact the responsible team.");
            }

            return result;
        }

        public Result<IEnumerable<Course>> GetAll()
        {
            var result = new Result<IEnumerable<Course>>();
            var content = new List<Course>();

            try
            {
                lock (_lock)
                {
                    foreach (var course in _courses)
                    {
                        content.Add(course.Clone() as Course);
                    }

                    result.Content = content;
                }
            }
            catch (Exception ex)
            {
                this._logger.Error(ex);
                result.AddError("An error occurred while get courses, please re-try.");
            }

            return result;
        }

        public Result<int> GetMaxId()
        {
            var result = new Result<int>();

            try
            {
                lock (_lock)
                {
                    if (_courses.Count() > 0)
                        result.Content = _courses.Max(c => c.Id);
                }
            }
            catch (Exception ex)
            {
                this._logger.Error(ex);
                result.AddError("An error occurred while get last course, please re-try.");
            }

            return result;
        }

        public Result AddStudent(Course course)
        {
            var result = new Result();

            try
            {
                lock (_lock)
                {
                    var key = _courses.First(c => c.Id == course.Id);

                    if (key.Students == null)
                        key.Students = new List<Student>();

                    (key.Students as IList).Add(course.Students.First());
                }
            }
            catch (Exception ex)
            {
                this._logger.Error(ex);
                result.AddError("An error occurred while add student to course, please re-try.");
            }

            return result;
        }
    }
}