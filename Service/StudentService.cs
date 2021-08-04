using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Interfaces.Visitors;
using Domain.Utils;
using log4net;

namespace Service
{
    public class StudentService : BaseService<Student>, IStudentService
    {
        readonly IMessageQueueVisitor<Student> _msgVisitor;

        readonly ICourseService _courseService;

        public StudentService(
            ILog logger,
            ICourseService courseService,
            IStudentRepository repository,
            IMessageQueueVisitor<Student> messageVisitor) : base(logger, repository)
        {
            this._courseService = courseService;
            this._msgVisitor = messageVisitor;
        }

        public override Result Delete(IEnumerable<Student> instances)
        {
            var result = new Result();

            result.AddError("Delete(students) method not implemented yet.");

            return result;
        }

        public override Result Insert(IEnumerable<Student> instances)
        {
            var result = new Result();

            result.AddError("Insert(students) method not implemented yet.");

            return result;
        }

        public override Result Update(IEnumerable<Student> instances)
        {
            var result = new Result();

            result.AddError("Update(students) method not implemented yet.");

            return result;
        }

        public async Task<Result> SignUp(Student student)
        {
            return await Task.Run<Result>(() =>
           {
               var result = new Result();

               try
               {
                   if (student == null)
                   {
                       result.AddError("Invalid student, please verify.");
                       return result;
                   }

                   Course course = student.Courses.First();

                   if (course == null || course.Id <= 0)
                   {
                       result.AddError("Course was not informed, please verify.");
                       return result;
                   }

                   var courseResult = this._courseService.GetById(course.Id);

                   if (courseResult.Error)
                   {
                       result.AddError(courseResult.Messages.First());
                       return result;
                   }

                   if (courseResult.Content.Students.Count() >= course.MaxNumberOfStudents)
                   {
                       result.AddError("The maximum number of registrations for the course has already been filled. " +
                               "Choose another one or wait for new classes :(.");

                       return result;
                   }

                   //Queue proccess...
                   this._msgVisitor.SetQueueParameters("localhost", 80);

                   try
                   {
                       this._msgVisitor.Visit(student);
                   }
                   catch (Exception ex)
                   {
                       this._logger.Error(ex);
                       result.AddError("There was an error when send your request, please re-try.");
                   }
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