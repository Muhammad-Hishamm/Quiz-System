using AutoMapper;
using AutoMapper.QueryableExtensions;
using Examination_System.DTOs.Courses;
using Examination_System.DTOs.Instructors;
using Examination_System.Models;
using Examination_System.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Examination_System.Services
{
    public class InstructorService
    {
        private readonly GeneralRepository<Instructor> _generalRepository;
        private readonly IMapper _mapper;

        public InstructorService(IMapper mapper)
        {
            _mapper = mapper;
            _generalRepository = new GeneralRepository<Instructor>(_mapper);
        }

        public async Task<IEnumerable<GetAllInstructorsDTOs>> GetAll(Expression<Func<Instructor, bool>>? filter = null)
        {
            return await _generalRepository.GetAll<GetAllInstructorsDTOs>(filter);
        }

        public async Task<GetAllInstructorsDTOs> GetById(int id)
        {
            var dto =await _generalRepository.GetById<GetAllInstructorsDTOs>(id);
            return dto;
        }

        public async Task<bool> Create(CreateInstructorDTO instructorDto)
        {
            var instructor = _mapper.Map<Instructor>(instructorDto);
            return await _generalRepository.CreateAsync(instructor).ConfigureAwait(false);
        }

        public async Task<bool> Update(int id, UpdateInstructorDto updatedInstructor)
        {
            if (updatedInstructor == null) return false;
            if (this.GetById(id) == null) return false;

            var instructor = _mapper.Map<Instructor>(updatedInstructor);
            //instructor.Id = id;

            var result = await _generalRepository.UpdateAsync(instructor).ConfigureAwait(false);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var existing = this.GetById(id);
            if (existing == null) return false;

            await _generalRepository.DeleteAsync(id).ConfigureAwait(false);
            return true;
        }
    }
}
