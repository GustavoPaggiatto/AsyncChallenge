using System;
using Domain.Enums;

namespace Domain.Interfaces.Visitors
{
    public interface IMessageQueueVisitor<T> : IVisitor<T>, IDisposable
    {
        void SetQueueParameters(string host, int port, int heartBeat = 20);
        void SetMode(QueueMode mode);
    }
}