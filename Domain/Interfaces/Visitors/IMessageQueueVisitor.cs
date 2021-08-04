namespace Domain.Interfaces.Visitors
{
    public interface IMessageQueueVisitor<T> : IVisitor<T>
    {
         void SetQueueParameters(string host, int port, int heartBeat = 20);
    }
}