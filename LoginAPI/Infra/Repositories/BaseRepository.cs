using LoginAPI.EF.Context;
using LoginAPI.Model.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LoginAPI.Infra.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly BaseContext _dbcontext;
        private DbSet<T> table;
        public BaseRepository(BaseContext dBContext)
        {
            _dbcontext = dBContext;
            table = _dbcontext.Set<T>();
        }

        public T Find(int id)
        {
            return table.Find(id);
        }

        public IQueryable<T> List()
        {
            return table;
        }

        public void Add(T item)
        {
            table.Add(item);
        }

        public void Remove(T item)
        {
            table.Remove(item);
        }

        public void Edit(T item)
        {
            _dbcontext.Entry(item).State = EntityState.Detached;
            _dbcontext.Entry(item).State = EntityState.Modified;
            _dbcontext.SaveChanges();
        }

        public void Dispose()
        {
            _dbcontext.Dispose();
        }
        public void Save()
        {
            _dbcontext.SaveChanges();
        }

        public T FirstOrDeafault(Expression<Func<T, bool>> clause)
        {
            return table.FirstOrDefault(clause);
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> clause)
        {
            return table.Where(clause);
        }
    }
}
