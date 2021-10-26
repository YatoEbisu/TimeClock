using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TimeClock.API.Context;
using TimeClock.API.Interfaces;

namespace TimeClock.API.Repositorys
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly PgContext _context;

        public BaseRepository(PgContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(T obj)
        {
            _context.Set<T>().Add(obj);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateAsync(T obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T obj)
        {
            _context.Set<T>().Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            var result = await _context.Set<T>().AsNoTracking().Where(predicate).ToListAsync();
            return result;
        }

        public async Task<T> FindOneAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);
        }
    }
}
