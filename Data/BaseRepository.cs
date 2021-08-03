using Domain.Interfaces.Repositories;
using Domain.Entities;
using Domain.Utils;
using log4net;
using System.Collections.Generic;

namespace Data
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        protected readonly ILog _logger;
        public BaseRepository(ILog logger)
        {
            this._logger = logger;
        }

        public abstract Result Insert(IEnumerable<T> instances);
        public abstract Result Update(IEnumerable<T> instances);
        public abstract Result Delete(IEnumerable<T> instances);

        public virtual void Dispose()
        {
            foreach (var appender in this._logger.Logger.Repository.GetAppenders())
                appender.Close();
        }
    }
}