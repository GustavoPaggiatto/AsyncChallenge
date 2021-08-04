using System.Net.Mail;

namespace Domain.Interfaces.Visitors
{
    public interface IMailVisitor : IVisitor<MailMessage>
    {
        void SetSptmConfiguration(string host, string user, string password, bool useSssl = false);
    }
}