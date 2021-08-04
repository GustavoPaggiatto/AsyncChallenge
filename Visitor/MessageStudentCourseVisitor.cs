using System;
using Domain.Interfaces.Visitors;
using Domain.Entities;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;

namespace Visitor
{
    public sealed class MessageStudentCourseVisitor : IMessageQueueVisitor<Student>
    {
        private ConnectionFactory _connFactory;

        public void SetQueueParameters(string host, int port, int heartBeat = 20)
        {
            this._connFactory = new ConnectionFactory();
            this._connFactory.HostName = host;
            this._connFactory.Port = port;
            this._connFactory.RequestedHeartbeat = new TimeSpan(0, 0, heartBeat);
        }

        public void Visit(Student instance)
        {
            using (var connection = _connFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("SignUps", true, false, false, null);

                    string message = JsonConvert.SerializeObject(instance);

                    byte[] stream = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish("", "SignUps", null, stream);
                }
            }
        }
    }
}
