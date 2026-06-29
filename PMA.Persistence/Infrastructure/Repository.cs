//using Microsoft.EntityFrameworkCore;
//using PMA.Domain;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace PMA.Persistence.Infrastructure
//{
//    public class Repository<T> : IRepository<T> where T : BaseEntity
//    {
//        protected readonly DbContext _context;
//        protected readonly DbSet<T> _dbSet;
//        public Repository(DbContext context)
//        {
//            _context = context;
//            _dbSet = context.Set<T>();
//        }
//        public async Task<List<T>> GetAllAsync()
//        {
//            return await _dbSet.ToListAsync();
//        }
//        public async Task<T> GetByIdAsync(Guid id)
//        {
//            return await _dbSet.FirstOrDefaultAsync(x => x.id == id);
//        }
//        public async Task AddAsync(T entity)
//        {
//            await _dbSet.AddAsync(entity);
//            await _context.SaveChangesAsync();
//        }
//        public void Update(T entity)
//        {
//            _dbSet.Update(entity);
//            _context.SaveChanges();
//        }
//        public void Delete(Guid id)
//        {
//            var entity = _dbSet.FirstOrDefault(x => x.id == id);
//            if (entity == null)
//                return;
//            _dbSet.Remove(entity);
//            _context.SaveChanges();
//        }
//    }
//}
