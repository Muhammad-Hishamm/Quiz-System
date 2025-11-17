using Examination_System.Data;
using Examination_System.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Examination_System.Repositories
{
    public class GeneralRepository <T> where T : BaseModel
    {
        
        Context _context;
        DbSet<T> _entities;
        public GeneralRepository()
        {
            _context = new Context();
            _entities = _context.Set<T>();
        }
        // this is get method for all entities
        public  IQueryable<T>  GetAll()
        {
            var ans =  _entities.Where(E => ! E.IsDeleted );
            return ans;
        }
        // this is to get by id 
        public async Task<T> GetByIDAsync(int id)
        {
            var entity = await  _entities.FirstOrDefaultAsync(e => e.Id == id && ! e.IsDeleted )
                         .ConfigureAwait(false);
            return entity;
        }

        // this method to create a new entity
        public async Task CreateAsync(T entity)
        {
           _entities.Add(entity);
           await _context.SaveChangesAsync();
        }

        // this endpoint to update the entity T
        public async Task  UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        // this endpoint to soft delete the entity with id 
        public async Task DeleteAsync(int id)
        {
            var entity = await _entities.AsTracking().FirstOrDefaultAsync(_e => _e.Id == id);
            entity.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}
