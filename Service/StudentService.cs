using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;
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
        readonly IMailVisitor<Student> _mailVisitor;
        readonly ICourseService _courseService;

        public StudentService(
            ILog logger,
            ICourseService courseService,
            IStudentRepository repository,
            IMessageQueueVisitor<Student> messageVisitor,
            IMailVisitor<Student> mailVisitor) : base(logger, repository)
        {
            this._courseService = courseService;
            this._msgVisitor = messageVisitor;
            this._mailVisitor = mailVisitor;
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

        public Task<Result> SignUp(Student student)
        {
            return Task.Run<Result>(() =>
           {
               var result = new Result();

               try
               {
                   if (student == null)
                   {
                       result.AddError("Invalid student, please verify.");
                       return result;
                   }

                   if (string.IsNullOrEmpty(student.Name))
                   {
                       result.AddError("Student name was not informed, please verify.");
                       return result;
                   }

                   if (string.IsNullOrEmpty(student.Email))
                   {
                       result.AddError("Student email was not informed, please verify.");
                       return result;
                   }

                   var regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");

                   if (!regex.IsMatch(student.Email))
                   {
                       result.AddError("Student email is invalid, please verify format(Ex.: gustavo@developer.com.br).");
                       return result;
                   }

                   if (student.BirthDate.Date.Equals(DateTime.MinValue.Date))
                   {
                       result.AddError("Student birthdate was not informed, please verify");
                       return result;
                   }

                   if (student.BirthDate.Date.Subtract(DateTime.Now.Date).Ticks >= 0)
                   {
                       result.AddError("Student birthdate must be minor than actual date, please verify.");
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

                   if (courseResult.Content == null)
                   {
                       result.AddError("Course not exists, please verify.");
                       return result;
                   }

                   if (courseResult.Content.Students != null)
                   {
                       if (courseResult.Content.Students.Count() >= courseResult.Content.MaxNumberOfStudents)
                       {
                           result.AddError("The maximum number of registrations for the course has already been filled. " +
                                   "Choose another one or wait for new classes :(.");

                           return result;
                       }
                   }

                   //Queue proccess...
                   this._msgVisitor.SetQueueParameters("localhost", 5672);
                   this._msgVisitor.SetMode(QueueMode.Input);

                   try
                   {
                       this._msgVisitor.Visit(student);
                       this._msgVisitor.Dispose();
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

        public void ProccessSignUp()
        {
            this._logger.Debug("Initializing queue read.");

            try
            {
                //Queue proccess...
                this._msgVisitor.SetQueueParameters("localhost", 5672, 20);
                this._msgVisitor.SetMode(QueueMode.Output);

                Student instance = this.New();
                this._msgVisitor.Visit(instance);

                if (instance == null)
                {
                    this._logger.Debug("JSON deserializing error (Student is null).");
                    return;
                }

                this._logger.Debug($"Student deserialized (Id: {instance.Id}; Name: {instance.Name}.)");

                var courseResult = this._courseService.GetById(instance.Courses.First().Id);

                if (courseResult.Error)
                    return;

                if (courseResult.Content == null)
                {
                    this._logger.Debug("Course not exists.");
                    return;
                }

                var courseSetter = courseResult.Content.Clone() as Course;
                courseSetter.Students = new List<Student>() { instance };

                var result = this._courseService.AddStudent(courseSetter);

                if (result.Error)
                {
                    this._logger.Error(result.Messages.First());
                    return;
                }

                // Send email...
                this._mailVisitor.SetSptmConfiguration("smtp.gmail.com", 587, true);
                this._mailVisitor.Visit(instance);
            }
            catch (Exception ex)
            {
                this._logger.Error(ex);
            }
            finally
            {
                this._logger.Debug("Finalizing queue read.");
            }
        }
    }
}