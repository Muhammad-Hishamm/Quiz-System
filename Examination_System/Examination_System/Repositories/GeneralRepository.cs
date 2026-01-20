using AutoMapper;
using AutoMapper.QueryableExtensions;
using Examination_System.Data;
using Examination_System.DTOs.Courses;
using Examination_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Examination_System.Repositories
{
    public class GeneralRepository<T> where T : BaseModel
    {
        private readonly Context _context;
        private readonly DbSet<T> _entities;
        private readonly IMapper _mapper;

        public GeneralRepository(IMapper mapper)
        {
            _context = new Context();
            _entities = _context.Set<T>();
            _mapper = mapper;
        }


        public async Task<IEnumerable<TDto>> GetAll<TDto>(Expression<Func<T, bool>>? filter = null)
        {
            filter ??= (Expression<Func<T, bool>>)(_ => true);

            var query = _entities.Where(e => !e.IsDeleted)
                                 .Where(filter); 

            var ret = await query
                .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                .ToListAsync()
                .ConfigureAwait(false);

            return ret;
        }

        // get by id
        public  async Task<TDto?> GetById<TDto>(int id)
        {
            var ret = await _entities.Where(e => e.Id == id && !e.IsDeleted)
                            .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                            .FirstOrDefaultAsync()
                            .ConfigureAwait(false);
            return ret;
        }


        // Create
        public async Task<bool> CreateAsync<DTO>(DTO dto)
        {
            T entity = _mapper.Map<T>(dto);
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
