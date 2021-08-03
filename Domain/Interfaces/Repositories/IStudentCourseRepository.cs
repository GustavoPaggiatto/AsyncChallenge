using Domain.Entities;
using Domain.Utils;

namespace Domain.Interfaces.Repositories
{
    public interface IStudentCourseRepository : IRepository<StudentCourse>
    {
         Result SignUp(StudentCourse instance);
    }
}