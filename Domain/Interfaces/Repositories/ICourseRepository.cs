using Domain.Entities;
using Domain.Utils;

namespace Domain.Interfaces.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
         Result<Course> GetById(int id);
    }
}