using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ServiceBase<T> : /*IDisposable,*/ IServiceBase<T> where T : class
    {
        private readonly IRepositoryBase<T> _repository;

        public ServiceBase(IRepositoryBase<T> repository)
        {
            _repository = repository;
        }

        public async Task<T> AddAsync(T entity)
        {
            var entityDB = await _repository.AddAsync(entity);
            return entityDB;
        }

        public async Task Delete(T entity)
        {
            await _repository.Delete(entity);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public T GetById(int id)
        {
            return _repository.GetById(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public List<T> SQLQueryStringList(string query)
        {
            return _repository.SQLQueryStringList(query);
        }

        public async Task<List<T>> SQLQueryStringListAsync(string query)
        {
            return await _repository.SQLQueryStringListAsync(query);
        }

        public T SQLQueryStringSingle(string query)
        {
            return _repository.SQLQueryStringSingle(query);
        }

        public async Task<T> SQLQueryStringSingleAsync(string query)
        {
            return await _repository.SQLQueryStringSingleAsync(query);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            return await _repository.UpdateAsync(entity);
        }
    }
}
