using System.Collections.Generic;
using Domain.Entities;
using Domain.Utils;
using Domain.Interfaces.Services;
using Domain.Interfaces.Repositories;
using log4net;
using System.Linq;
using Domain.Results;
using System;
using Domain.Interfaces.Adapters;

namespace Service
{
    public sealed class CourseService : BaseService<Course>, ICourseService
    {
        readonly IAdapter<CourseGeneralReport> _reportAdapter;

        public CourseService(
            ILog logger,
            ICourseRepository repository,
            IAdapter<CourseGeneralReport> adapter) : base(logger, repository)
        {
            this._reportAdapter = adapter;
        }

        public override Result Insert(IEnumerable<Course> instances)
        {
            var result = new Result();

            if (instances == null || instances.Count() == 0)
            {
                return result;
            }

            var resultLast = (this._repository as ICourseRepository).GetMaxId();

            if (resultLast.Error)
            {
                result.AddError(resultLast.Messages.First());
                return result;
            }

            int lastId = resultLast.Content;

            foreach (var course in instances)
            {
                course.Id = ++lastId;

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

        public Result<IEnumerable<CourseGeneralReport>> GetDefaultReport()
        {
            var result = new Result<IEnumerable<CourseGeneralReport>>();

            try
            {
                var coursesResult = (this._repository as ICourseRepository).GetAll();

                if (coursesResult.Error)
                {
                    result.AddError(coursesResult.Messages.First());
                    return result;
                }

                var report = new List<CourseGeneralReport>();

                foreach (var course in coursesResult.Content)
                {
                    var reportResult = this._reportAdapter.Adaptee(course);

                    if (reportResult.Error)
                    {
                        result.AddError(reportResult.Messages.First());
                        return result;
                    }

                    report.Add(reportResult.Content);
                }

                result.Content = report;
            }
            catch (Exception ex)
            {
                this._logger.Error(ex);
                result.AddError("An error occurred while mounting report, please re-try.");
            }

            return result;
        }

        public Result AddStudent(Course course)
        {
            var result = new Result();

            if (course.Students == null || course.Students.Count() == 0)
            {
                result.AddError("Student not informed.");
                return result;
            }

            var courseResult = this.GetById(course.Id);

            if (courseResult.Error)
            {
                result.AddError(courseResult.Messages.First());
                return result;
            }

            if (courseResult.Content == null)
            {
                result.AddError("This course not exists anymore.");
                return result;
            }

            if (courseResult.Content.Students.Count() >= courseResult.Content.MaxNumberOfStudents)
            {
                result.AddError("There are no more vacancies for this course.");
                return result;
            }

            (this._repository as ICourseRepository).AddStudent(course);

            return result;
        }
    }
}