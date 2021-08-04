using System.Threading.Tasks;
using Domain.Entities;
using Domain.Utils;

namespace Domain.Interfaces.Services
{
    public interface IStudentService : IService<Student>
    {
         Task<Result> SignUp(Student student);
    }
}