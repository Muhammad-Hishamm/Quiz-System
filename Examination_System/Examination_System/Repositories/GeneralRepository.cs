using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Examination_System.Data;
using Examination_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Examination_System.Repositories
{
    public class GeneralRepository<T> where T : BaseModel
    {
        private readonly Context _context;
        private readonly DbSet<T> _entities;
        private bool _disposed;

        public GeneralRepository()
        {
            _context = new Context();
            _entities = _context.Set<T>();
        }

        // get all
        public IQueryable<T> GetAll()
        {
            var querey = _entities.Where(e => !e.IsDeleted);
            return querey;
        }
        public IQueryable<TDto>? GetAll<TDto>(Expression<Func<T, TDto>> selector)
        {
            return _entities.Where(e => !e.IsDeleted).Select(selector);
        }
        // get by id
        public  IQueryable<T> GetById(int id)
        {
            return  _entities.Where(e => e.Id == id && !e.IsDeleted) ;
        }
        // can i make task<iqueryable<T>>


        //public async Task<TDto> GetByIdAsync<TDto>(int id, Expression<Func<T, TDto>> selector)
        //{
        //    return await _entities
        //        .Where(e => e.Id == id && !e.IsDeleted)
        //        .Select(selector)
        //        .FirstOrDefaultAsync()
        //        .ConfigureAwait(false);
        //}

        // Create
        public async Task<bool> CreateAsync(T entity)
        {
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }

        // Update
        public async Task<bool> UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }

        // Soft delete
        public async Task DeleteAsync(int id)
        {
            var entity = await _entities
                .AsTracking()
                .FirstOrDefaultAsync(_e => _e.Id == id)
                .ConfigureAwait(false);
            if (entity is not null)
            {
                entity.IsDeleted = true;
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
        }
    }
}
