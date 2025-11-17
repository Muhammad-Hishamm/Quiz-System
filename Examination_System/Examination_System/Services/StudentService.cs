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

        public IQueryable<Student> GetAll()
        {
            return _studentRepository.GetAll();
        }

        public async Task<Student> GetById(int id)
        {
            return await _studentRepository.GetByIDAsync(id);
        }

        public async Task<bool> Create(Student student)
        {
            if (student == null) return false;
            await _studentRepository.CreateAsync(student);
            return true;
        }

        public async Task<bool> Update(int id, Student updatedStudent)
        {
            if (updatedStudent == null) return false;

            var existing = await _studentRepository.GetByIDAsync(id);
            if (existing == null) return false;

            existing.Name = updatedStudent.Name;
            existing.Email = updatedStudent.Email;

            await _studentRepository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var existing = await GetById(id);
            if (existing == null) return false;
            await _studentRepository.DeleteAsync(id);
            return true;
        }
    }
}