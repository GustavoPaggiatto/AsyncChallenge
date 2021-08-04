using Domain.Entities;

namespace Domain.Interfaces.Visitors
{
    public interface IVisitor<T>
    {
         void Visit(T instance);
    }
}