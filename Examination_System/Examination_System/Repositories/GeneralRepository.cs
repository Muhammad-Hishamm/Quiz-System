using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Examination_System.Data;
using Examination_System.Models;
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
            return _entities.Where(e => !e.IsDeleted);
        }
        public IQueryable<TDto>? GetAll<TDto>(Expression<Func<T, TDto>> selector)
        {
            return _entities.Where(e => !e.IsDeleted).Select(selector);
        }
        // get by id
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _entities.Where(e => e.Id == id && !e.IsDeleted)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }
        public async Task<TDto?> GetByIdAsync<TDto>(int id,Expression<Func<T,TDto>> selector)
        {
            return await _entities
                .Where(e => e.Id == id && !e.IsDeleted)
                .Select(selector)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        // Create
        public async Task CreateAsync(T entity)
        {
            _entities.Add(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        // Update
        public async Task UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
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
