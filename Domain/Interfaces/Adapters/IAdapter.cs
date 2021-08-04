using Domain.Utils;

namespace Domain.Interfaces.Adapters
{
    public interface IAdapter<T>
    {
         Result<T> Adaptee(params object[] parameters);
    }
}