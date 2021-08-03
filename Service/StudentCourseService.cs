using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Interfaces.Repositories;
using log4net;
using Domain.Utils;
using System.Linq;
using Domain.Interfaces.Visitors;

namespace Service
{
    public sealed class StudentCourseService : BaseService<StudentCourse>, IStudentCourseService
    {
        private static Dictionary<int, int> _courseLogins;

        private readonly ICourseService _courseService;
        private readonly IMessageVisitor _msgVisitor;

        public StudentCourseService(
            ILog logger,
            IStudentCourseRepository repository,
            ICourseService courseService,
            IMessageVisitor msgVisitor) : base(logger, repository)
        {
            this._courseService = courseService;
            this._msgVisitor = msgVisitor;
        }

        static StudentCourseService()
        {
            _courseLogins = new Dictionary<int, int>();
        }

        public override Result Insert(IEnumerable<StudentCourse> instances)
        {
            var result = new Result();

            result.AddError("Insert(studentCourse) method not implemented yet.");

            return result;
        }

        public override Result Update(IEnumerable<StudentCourse> instances)
        {
            var result = new Result();

            result.AddError("Update(studentCourse) method not implemented yet.");

            return result;
        }

        public override Result Delete(IEnumerable<StudentCourse> instances)
        {
            var result = new Result();

            result.AddError("Delete(studentCourse) method not implemented yet.");

            return result;
        }

        public async Task<Result> SignUp(StudentCourse instance)
        {
            return await Task.Run<Result>(() =>
            {
                var result = new Result();

                try
                {
                    if (instance == null)
                        return result;

                    if (instance.Course == null || instance.Course.Id <= 0)
                    {
                        result.AddError("Course was not informed, please verify.");
                        return result;
                    }

                    if (instance.Student == null)
                    {
                        result.AddError("Invalid student, please verify.");
                        return result;
                    }

                    var courseResult = this._courseService.GetById(instance.Course.Id);

                    if (courseResult.Error)
                    {
                        result.AddError(courseResult.Messages.First());
                        return result;
                    }

                    var course = courseResult.Content;

                    if (course == null)
                    {
                        result.AddError("Invalid course (not exists).");
                        return result;
                    }

                    if (_courseLogins[course.Id] != 0 &&
                        _courseLogins[course.Id] >= course.MaxNumberOfStudents)
                    {
                        result.AddError("The maximum number of registrations for the course has already been filled. " +
                                "Choose another one or wait for new classes :(.");

                        return result;
                    }

                    //Queue proccess...
                    this._msgVisitor.SendSignUpUser(instance);
                }
                catch (Exception ex)
                {
                    this._logger.Error(ex);
                    result.AddError("There was an error when applying for the course, please try again.");
                }

                return result;
            });
        }
    }
}