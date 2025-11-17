using Examination_System.Models;
using Examination_System.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Examination_System.Services
{
    public class QuestionService
    {
        private readonly GeneralRepository<Question> _questionRepository;

        public QuestionService()
        {
            _questionRepository = new GeneralRepository<Question>();
        }

        public IQueryable<Question> GetAll()
        {
            return _questionRepository.GetAll();
        }

        public async Task<Question> GetById(int id)
        {
            return await _questionRepository.GetByIDAsync(id);
        }

        public async Task<bool> Create(Question question)
        {
            if (question == null) return false;
            await _questionRepository.CreateAsync(question);
            return true;
        }

        public async Task<bool> Update(int id, Question updatedQuestion)
        {
            if (updatedQuestion == null) return false;

            var existing = await _questionRepository.GetByIDAsync(id);
            if (existing == null) return false;

            existing.Level = updatedQuestion.Level;
            existing.QuestionBody = updatedQuestion.QuestionBody;
            existing.InstructorId = updatedQuestion.InstructorId;

            await _questionRepository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var existing = await GetById(id);
            if (existing == null) return false;
            await _questionRepository.DeleteAsync(id);
            return true;
        }
    }
}