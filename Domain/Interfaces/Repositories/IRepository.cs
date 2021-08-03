using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Utils;

namespace Domain.Interfaces.Repositories
{
    public interface IRepository<T> : IDisposable where T : BaseEntity, new()
    {
         Result Insert(IEnumerable<T> instances);
         Result Update(IEnumerable<T> instances);
         Result Delete(IEnumerable<T> instances);
    }
}