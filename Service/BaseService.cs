using System;
using log4net;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Interfaces.Repositories;
using Domain.Utils;

namespace Service
{
    public abstract class BaseService<T> : IService<T> where T : BaseEntity, new()
    {
        protected readonly ILog _logger;
        protected readonly IRepository<T> _repository;

        public BaseService(ILog logger, IRepository<T> repository)
        {
            this._logger = logger;
            this._repository = repository;
        }

        public virtual T New()
        {
            return new T();
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
