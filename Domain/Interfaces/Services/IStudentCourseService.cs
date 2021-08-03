using Domain.Entities;
using Domain.Utils;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IStudentCourseService : IService<StudentCourse>
    {
         Task<Result> SignUp(StudentCourse instance);
    }
}