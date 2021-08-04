using System.Collections.Generic;
using Domain.Entities;
using Domain.Utils;

namespace Domain.Interfaces.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
         Result<Course> GetById(int id);
         Result<int> GetLogins(int courseId);
         Result<IEnumerable<Course>> GetAll();
    }
}