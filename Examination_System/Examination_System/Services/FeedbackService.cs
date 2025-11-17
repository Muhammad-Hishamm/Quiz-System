using Examination_System.Models;
using Examination_System.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Examination_System.Services
{
    public class FeedbackService
    {
        private readonly GeneralRepository<Feedback> _feedbackRepository;

        public FeedbackService()
        {
            _feedbackRepository = new GeneralRepository<Feedback>();
        }

        public IQueryable<Feedback> GetAll()
        {
            return _feedbackRepository.GetAll();
        }

        public async Task<Feedback> GetById(int id)
        {
            return await _feedbackRepository.GetByIDAsync(id);
        }

        public async Task<bool> Create(Feedback feedback)
        {
            if (feedback == null) return false;
            await _feedbackRepository.CreateAsync(feedback);
            return true;
        }

        public async Task<bool> Update(int id, Feedback updatedFeedback)
        {
            if (updatedFeedback == null) return false;

            var existing = await _feedbackRepository.GetByIDAsync(id);
            if (existing == null) return false;

            existing.Rating = updatedFeedback.Rating;
            existing.Comments = updatedFeedback.Comments;
            existing.ResultId = updatedFeedback.ResultId;

            await _feedbackRepository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var existing = await GetById(id);
            if (existing == null) return false;
            await _feedbackRepository.DeleteAsync(id);
            return true;
        }
    }
}