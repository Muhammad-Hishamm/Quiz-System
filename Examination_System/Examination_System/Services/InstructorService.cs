using Examination_System.DTOs.Instructors;
using Examination_System.DTOs.Instructors;
using Examination_System.Models;
using Examination_System.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Examination_System.Services
{
    public class InstructorService
    {
        private readonly GeneralRepository<Instructor> _instructorRepository;

        public InstructorService()
        {
            _instructorRepository = new GeneralRepository<Instructor>();
        }

        public IQueryable<GetAllInstructorsDTOs>? GetAll()
        {
            return _instructorRepository.GetAll<GetAllInstructorsDTOs>(i => new GetAllInstructorsDTOs
            {
                Id = i.Id,
                Name = i.Name,
                Email = i.Email,
                Department = i.Department
            });
        }

        public Task<GetAllInstructorsDTOs?> GetByIdAsync(int id)
        {
            return _instructorRepository.GetByIdAsync<GetAllInstructorsDTOs>(id, i => new GetAllInstructorsDTOs
            {
                Id = i.Id,
                Name = i.Name,
                Email = i.Email,
                Department = i.Department
            });
        }

        public async Task<bool> Create(CreateInstructorDTO instructorDto)
        {
            if (instructorDto is null) return false;

            var instructor = new Instructor
            {
                Name = instructorDto.Name,
                Email = instructorDto.Email,
                Department = instructorDto.Department
            };

            await _instructorRepository.CreateAsync(instructor).ConfigureAwait(false);
            return true;
        }

        public async Task<bool> Update(int id, UpdateInstructorDto updatedInstructor)
        {
            if (updatedInstructor == null) return false;

            var existing = await _instructorRepository.GetByIdAsync(id).ConfigureAwait(false);
            if (existing == null) return false;

            existing.Name = updatedInstructor.Name;
            existing.Email = updatedInstructor.Email;
            existing.Department = updatedInstructor.Department;

            await _instructorRepository.UpdateAsync(existing).ConfigureAwait(false);
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var existing = await _instructorRepository.GetByIdAsync(id).ConfigureAwait(false);
            if (existing == null) return false;

            await _instructorRepository.DeleteAsync(id).ConfigureAwait(false);
            return true;
        }
    }
}
