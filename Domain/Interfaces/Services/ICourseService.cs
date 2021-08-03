using Domain.Entities;
using Domain.Utils;

namespace Domain.Interfaces.Services
{
    public interface ICourseService : IService<Course>
    {
        Result<Course> GetById(int id);
    }
}