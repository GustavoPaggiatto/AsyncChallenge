using System;
using Domain.Interfaces.Visitors;
using Domain.Entities;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;
using Domain.Enums;
using RabbitMQ.Client.Events;

namespace Visitor
{
    public sealed class MessageStudentCourseVisitor : IMessageQueueVisitor<Student>
    {
        private ConnectionFactory _connFactory;
        private QueueMode _mode;
        private IConnection _connection;
        private IModel _model;

        public void SetQueueParameters(string host, int port, int heartBeat = 20)
        {
            this._connFactory = new ConnectionFactory();
            this._connFactory.HostName = host;
            this._connFactory.Port = port;
            this._connFactory.RequestedHeartbeat = new TimeSpan(0, 0, heartBeat);
            this._connection = _connFactory.CreateConnection();
            this._model = this._connection.CreateModel();
        }

        public void SetMode(QueueMode mode)
        {
            this._mode = mode;
        }

        public void Visit(Student instance)
        {
            _model.QueueDeclare("SignUps", true, false, false, null);

            if (this._mode == QueueMode.Input)
            {
                string message = JsonConvert.SerializeObject(instance);

                byte[] stream = Encoding.UTF8.GetBytes(message);

                _model.BasicPublish("", "SignUps", null, stream);
            }
            else
            {
                var msgResult = _model.BasicGet("SignUps", true);

                if (msgResult != null)
                {
                    var body = msgResult.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var temp = JsonConvert.DeserializeObject<Student>(message);

                    instance.Email = temp.Email;
                    instance.Name = temp.Name;
                    instance.BirthDate = temp.BirthDate;
                    instance.Courses = temp.Courses;
                }
            }
        }

        public void Dispose()
        {
            this._model.Dispose();
            this._connection.Dispose();
        }
    }
}
