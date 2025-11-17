using Examination_System.Models;
using Examination_System.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Examination_System.Services
{
    public class ExamService
    {
        private readonly GeneralRepository<Exam> _examRepository;

        public ExamService()
        {
            _examRepository = new GeneralRepository<Exam>();
        }

        public IQueryable<Exam> GetAll()
        {
            return _examRepository.GetAll();
        }

        public async Task<Exam> GetById(int id)
        {
            return await _examRepository.GetByIDAsync(id);
        }

        public async Task<bool> Create(Exam exam)
        {
            if (exam == null) return false;
            await _examRepository.CreateAsync(exam);
            return true;
        }

        public async Task<bool> Update(int id, Exam updatedExam)
        {
            if (updatedExam == null) return false;

            var existing = await _examRepository.GetByIDAsync(id);
            if (existing == null) return false;

            existing.Title = updatedExam.Title;
            existing.Type = updatedExam.Type;
            existing.NumberOfQuestions = updatedExam.NumberOfQuestions;
            existing.CourseId = updatedExam.CourseId;

            await _examRepository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var existing = await GetById(id);
            if (existing == null) return false;
            await _examRepository.DeleteAsync(id);
            return true;
        }
    }
}