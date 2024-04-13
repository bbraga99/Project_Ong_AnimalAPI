﻿using System.Linq.Expressions;

namespace Ong_AnimalAPI.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(Expression<Func<T, bool>> predicate) ;
        T Create(T entity);
        T Update(T entity);
        T Delete(T entity);   
    }
}
