using System.Collections.Generic;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Utils;
using log4net;

namespace Data
{
    public sealed class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(ILog logger) : base(logger)
        {
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
    }
}