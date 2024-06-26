﻿using Microsoft.EntityFrameworkCore;
using Ong_AnimalAPI.Context;
using Ong_AnimalAPI.Models;
using System.Linq.Expressions;

namespace Ong_AnimalAPI.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }
        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            //_context.SaveChanges();
            return entity;
        }
        public T Update(T entity)
        {
            _context.Set<T>().Entry(entity).State = EntityState.Modified;
            //_context.SaveChanges();
            return entity;
        }
        public T Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            //_context.SaveChanges();
            return entity;
        }

    }
}
