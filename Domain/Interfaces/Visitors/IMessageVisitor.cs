using Domain.Entities;

namespace Domain.Interfaces.Visitors
{
    public interface IMessageVisitor
    {
         void SendSignUpUser(StudentCourse instance);
    }
}