using System.Collections.Generic;
using Domain.Entities;
using Domain.Utils;

namespace Domain.Interfaces.Services
{
    public interface ICourseService : IService<Course>
    {
        Result<Course> GetById(int id);
        Result<IEnumerable<Course>> GetAll();
    }
}