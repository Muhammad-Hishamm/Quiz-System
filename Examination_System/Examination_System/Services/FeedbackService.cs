using Examination_System.DTOs.Feedbacks;
using Examination_System.Models;
using Examination_System.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Examination_System.Services
{
    public class FeedbackService
    {
        private readonly GeneralRepository<Feedback> _repository;

        public FeedbackService()
        {
            _repository = new GeneralRepository<Feedback>();
        }

        public IQueryable<GetAllFeedbacksDTOs>? GetAll()
        {
            return _repository.GetAll<GetAllFeedbacksDTOs>(f => new GetAllFeedbacksDTOs
            {
                Id = f.Id,
                Rating = f.Rating,
                Comments = f.Comments,
                ResultId = f.ResultId
            });
        }

        public Task<GetAllFeedbacksDTOs?> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync<GetAllFeedbacksDTOs>(id, f => new GetAllFeedbacksDTOs
            {
                Id = f.Id,
                Rating = f.Rating,
                Comments = f.Comments,
                ResultId = f.ResultId
            });
        }
        public async Task<bool> Create(CreateFeedbackDTO dto)
        {
            if (dto is null) return false;

            var entity = new Feedback
            {
                Rating = dto.Rating,
                Comments = dto.Comments,
                ResultId = dto.ResultId
            };

            await _repository.CreateAsync(entity).ConfigureAwait(false);
            return true;
        }

        public async Task<bool> Update(int id, UpdateFeedbackDto dto)
        {
            if (dto is null) return false;

            // Retrieve the tracked entity (entity-returning overload)
            var entityToUpdate = await _repository.GetByIdAsync(id).ConfigureAwait(false);
            if (entityToUpdate == null) return false;

            entityToUpdate.Rating = dto.Rating;
            entityToUpdate.Comments = dto.Comments;
            entityToUpdate.ResultId = dto.ResultId;

            await _repository.UpdateAsync(entityToUpdate).ConfigureAwait(false);
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var existing = await this.GetByIdAsync(id).ConfigureAwait(false);
            if (existing == null) return false;
            await _repository.DeleteAsync(id).ConfigureAwait(false);
            return true;
        }
    }
}