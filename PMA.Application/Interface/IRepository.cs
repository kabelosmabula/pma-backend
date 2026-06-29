using PMA.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMA.Application.Interface
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(Guid id);
    }
}
