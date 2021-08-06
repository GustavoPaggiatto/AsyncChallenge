using System.Net;
using System.Net.Mail;
using Domain.Entities;
using Domain.Interfaces.Visitors;

namespace Visitor
{
    public sealed class StudentMailVisitor : IMailVisitor<Student>
    {
        private SmtpClient _smtp;

        public StudentMailVisitor()
        {
            this._smtp = new SmtpClient();
        }
        public void SetSptmConfiguration(string host, int port, bool useSssl = false)
        {
            this._smtp = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential("devpaggiatto@gmail.com", "DEV2021paggiatto"),
                EnableSsl = useSssl,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
        }

        public void Visit(Student instance)
        {
            MailMessage mail = new MailMessage(
                "devpaggiatto@gmail.com",
                instance.Email,
                "SignUp Course Info.",
                "Success SignUp Proccess :) ...");

            mail.IsBodyHtml = true;
            this._smtp.Send(mail);
        }
    }
}