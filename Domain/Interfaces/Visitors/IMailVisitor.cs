namespace Domain.Interfaces.Visitors
{
    public interface IMailVisitor<T> : IVisitor<T>
    {
        void SetSptmConfiguration(string host, int port, bool useSssl = false);
    }
}