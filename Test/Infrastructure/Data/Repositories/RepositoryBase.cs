using Domain.Interfaces.Repositories;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T>/*, IDisposable*/ where T : class
    {
        protected DbContextOptions<DatabaseContext> _databaseContext;
        protected DbSet<T> _dataSet;
        protected DatabaseContext _context;

        public RepositoryBase()
        {
            _databaseContext = new DbContextOptions<DatabaseContext>();

            _dataSet = new DatabaseContext(new DbContextOptions<DatabaseContext>()).Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                using (_context = new DatabaseContext(_databaseContext))
                {
                    _dataSet = _context.Set<T>();

                    _dataSet.Add(entity);
                    await _context.SaveChangesAsync();
                    return entity;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Remove(T entity)
        {
            try
            {
                using (_context = new DatabaseContext(_databaseContext))
                {
                    _dataSet = _context.Set<T>();
                    _dataSet.Remove(entity);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dataSet.ToListAsync();
        }

        public T GetById(int id)
        {
            return _dataSet.Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dataSet.FindAsync(id);
        }

        public List<T> SQLQueryStringList(string query)
        {
            return _dataSet.FromSqlRaw<T>(query).ToList();
        }

        public async Task<List<T>> SQLQueryStringListAsync(string query)
        {
            return await _dataSet.FromSqlRaw<T>(query).ToListAsync();
        }

        public T SQLQueryStringSingle(string query)
        {
            return _dataSet.FromSqlRaw<T>(query).FirstOrDefault();
        }

        public async Task<T> SQLQueryStringSingleAsync(string query)
        {
            return await _dataSet.FromSqlRaw<T>(query).FirstOrDefaultAsync();
        }

        public async Task<T> EditAsync(T entity)
        {
            try
            {
                using (_context = new DatabaseContext(_databaseContext))
                {
                    _dataSet = _context.Set<T>();
                    _dataSet.Update(entity);
                    await _context.SaveChangesAsync();
                    return entity;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
