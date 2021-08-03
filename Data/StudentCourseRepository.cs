using System.Collections.Generic;
using Domain.Utils;
using Domain.Entities;
using log4net;
using Domain.Interfaces.Repositories;

namespace Data
{
    public class StudentCourseRepository : BaseRepository<StudentCourse>, IStudentCourseRepository
    {
        public StudentCourseRepository(ILog logger) : base(logger)
        {
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

        public Result SignUp(StudentCourse instance)
        {
            return null;
        }
    }
}