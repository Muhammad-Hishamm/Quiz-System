using Examination_System.DTOs.Choices;
using Examination_System.Models;
using Examination_System.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Examination_System.Services
{
    public class ChoiceService
    {
        private readonly GeneralRepository<Choice> _choiceRepository;

        public ChoiceService()
        {
            _choiceRepository = new GeneralRepository<Choice>();
        }

        public IQueryable<GetAllChoicesDTOs>? GetAll()
        {
            return _choiceRepository.GetAll<GetAllChoicesDTOs>(c => new GetAllChoicesDTOs
            {
                Id = c.Id,
                ChoiceBody = c.ChoiceBody,
                IsCorrect = c.IsCorrect,
                QuestionId = c.QuestionId
            });
        }

        public Task<GetAllChoicesDTOs?> GetByIdAsync(int id)
        {
            return _choiceRepository.GetByIdAsync<GetAllChoicesDTOs>(id, c => new GetAllChoicesDTOs
            {
                Id = c.Id,
                ChoiceBody = c.ChoiceBody,
                IsCorrect = c.IsCorrect,
                QuestionId = c.QuestionId
            });
        }

        public async Task<bool> Create(CreateChoiceDTO dto)
        {
            if (dto is null) return false;

            var choice = new Choice
            {
                ChoiceBody = dto.ChoiceBody,
                IsCorrect = dto.IsCorrect,
                QuestionId = dto.QuestionId
            };

            await _choiceRepository.CreateAsync(choice).ConfigureAwait(false);
            return true;
        }

        public async Task<bool> Update(int id, UpdateChoiceDto updatedChoice)
        {
            if (updatedChoice == null) return false;

            var existing = await _choiceRepository.GetByIdAsync(id).ConfigureAwait(false);
            if (existing == null) return false;

            existing.ChoiceBody = updatedChoice.ChoiceBody;
            existing.IsCorrect = updatedChoice.IsCorrect;
            existing.QuestionId = updatedChoice.QuestionId;

            await _choiceRepository.UpdateAsync(existing).ConfigureAwait(false);
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var existing = await _choiceRepository.GetByIdAsync(id).ConfigureAwait(false);
            if (existing == null) return false;

            await _choiceRepository.DeleteAsync(id).ConfigureAwait(false);
            return true;
        }
    }
}