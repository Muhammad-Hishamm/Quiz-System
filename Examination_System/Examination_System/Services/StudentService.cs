using Examination_System.DTOs.Students;
using Examination_System.DTOs.Students;
using Examination_System.Models;
using Examination_System.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Examination_System.Services
{
    public class StudentService
    {
        private readonly GeneralRepository<Student> _studentRepository;

        public StudentService()
        {
            _studentRepository = new GeneralRepository<Student>();
        }

        public IQueryable<GetAllStudentsDTOs>? GetAll()
        {
            return _studentRepository.GetAll<GetAllStudentsDTOs>(s => new GetAllStudentsDTOs
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email
            });
        }

        public Task<GetAllStudentsDTOs?> GetByIdAsync(int id)
        {
            return _studentRepository.GetByIdAsync<GetAllStudentsDTOs>(id, s => new GetAllStudentsDTOs
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email
            });
        }

        public async Task<bool> Create(CreateStudentDTO dto)
        {
            if (dto is null) return false;

            var student = new Student
            {
                Name = dto.Name,
                Email = dto.Email
            };

            await _studentRepository.CreateAsync(student).ConfigureAwait(false);
            return true;
        }

        public async Task<bool> Update(int id, UpdateStudentDto updated)
        {
            if (updated == null) return false;

            var existing = await _studentRepository.GetByIdAsync(id).ConfigureAwait(false);
            if (existing == null) return false;

            existing.Name = updated.Name;
            existing.Email = updated.Email;

            await _studentRepository.UpdateAsync(existing).ConfigureAwait(false);
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var existing = await _studentRepository.GetByIdAsync(id).ConfigureAwait(false);
            if (existing == null) return false;

            await _studentRepository.DeleteAsync(id).ConfigureAwait(false);
            return true;
        }
    }
}