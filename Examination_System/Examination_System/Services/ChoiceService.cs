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

        public IQueryable<Choice> GetAll()
        {
            return _choiceRepository.GetAll();
        }

        public async Task<Choice> GetById(int id)
        {
            return await _choiceRepository.GetByIDAsync(id);
        }

        public async Task<bool> Create(Choice choice)
        {
            if (choice == null) return false;
            await _choiceRepository.CreateAsync(choice);
            return true;
        }

        public async Task<bool> Update(int id, Choice updatedChoice)
        {
            if (updatedChoice == null) return false;

            var existing = await _choiceRepository.GetByIDAsync(id);
            if (existing == null) return false;

            existing.ChoiceBody = updatedChoice.ChoiceBody;
            existing.IsCorrect = updatedChoice.IsCorrect;
            existing.QuestionId = updatedChoice.QuestionId;

            await _choiceRepository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var existing = await GetById(id);
            if (existing == null) return false;
            await _choiceRepository.DeleteAsync(id);
            return true;
        }
    }
}