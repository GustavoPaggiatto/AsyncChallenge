using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Utils;

namespace Domain.Interfaces.Services
{
    public interface IService<T> : IDisposable where T : BaseEntity, new()
    {
         T New();
         Result Insert(IEnumerable<T> instances);
         Result Update(IEnumerable<T> instances);
         Result Delete(IEnumerable<T> instances);
    }
}