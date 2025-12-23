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
                ChoiceBody = c.ChoiceBody
            });
        }

        public Task<GetAllChoicesDTOs?> GetByIdAsync(int id)
        {
            return _choiceRepository.GetByIdAsync<GetAllChoicesDTOs>(id, c => new GetAllChoicesDTOs
            {
                Id = c.Id,
                ChoiceBody = c.ChoiceBody
            });
        }

        public async Task<bool> Create(CreateChoiceDTO dto)
        {
            if (dto is null) return false;

            var choice = new Choice
            {
                ChoiceBody = dto.ChoiceBody
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