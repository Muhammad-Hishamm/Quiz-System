using Examination_System.DTOs.Questions;
using Examination_System.DTOs.Questions;
using Examination_System.Models;
using Examination_System.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Examination_System.Services
{
    public class QuestionService
    {
        private readonly GeneralRepository<Question> _generalRepository;

        public QuestionService()
        {
            _generalRepository = new GeneralRepository<Question>();
        }

        public IQueryable<GetAllQuestionsDTOs>? GetAll()
        {
            return _generalRepository.GetAll<GetAllQuestionsDTOs>(q => new GetAllQuestionsDTOs
            {
                Id = q.Id,
                Level = q.Level,
                QuestionBody = q.QuestionBody,
                InstructorId = q.InstructorId
            });
        }

        public Task<GetAllQuestionsDTOs?> GetByIdAsync(int id)
        {
            return _generalRepository.GetByIdAsync<GetAllQuestionsDTOs>(id, q => new GetAllQuestionsDTOs
            {
                Id = q.Id,
                Level = q.Level,
                QuestionBody = q.QuestionBody,
                InstructorId = q.InstructorId
            });
        }

        public async Task<bool> Create(CreateQuestionDTO dto)
        {
            if (dto is null) return false;

            var question = new Question
            {
                Level = dto.Level,
                QuestionBody = dto.QuestionBody,
                InstructorId = dto.InstructorId
            };

            await _generalRepository.CreateAsync(question).ConfigureAwait(false);
            return true;
        }

        public async Task<bool> Update(int id, UpdateQuestionDto updated)
        {
            if (updated == null) return false;

            var existing = await _generalRepository.GetByIdAsync(id).ConfigureAwait(false);
            if (existing == null) return false;

            existing.Level = updated.Level;
            existing.QuestionBody = updated.QuestionBody;
            existing.InstructorId = updated.InstructorId;

            await _generalRepository.UpdateAsync(existing).ConfigureAwait(false);
            return true;
        }

        public async Task<bool> Delete(int id)      
        {
            var existing = await _generalRepository.GetByIdAsync(id).ConfigureAwait(false);
            if (existing == null) return false;

            await _generalRepository.DeleteAsync(id).ConfigureAwait(false);
            return true;
        }
    }
}