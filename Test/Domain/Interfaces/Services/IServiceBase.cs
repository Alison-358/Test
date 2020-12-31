using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IServiceBase<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task<T> GetByIdAsync(int id);
        Task<T> UpdateAsync(T entity);
        Task Delete(T entity);
        Task<List<T>> SQLQueryStringListAsync(string query);
        Task<T> SQLQueryStringSingleAsync(string query);
        T GetById(int id);
        List<T> SQLQueryStringList(string query);
        T SQLQueryStringSingle(string query);
    }
}
