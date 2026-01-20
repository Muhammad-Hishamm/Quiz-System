using AutoMapper;
using Examination_System.DTOs.Feedbacks;
using Examination_System.Models;
using Examination_System.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Examination_System.Services
{
    public class FeedbackService
    {
        private readonly GeneralRepository<Feedback> _repository;
        private readonly IMapper _mapper;

        public FeedbackService(IMapper mapper)
        {
            _mapper = mapper;
            _repository = new GeneralRepository<Feedback>(_mapper);
        }

        public async Task<IEnumerable<GetAllFeedbacksDTOs>> GetAll()
        {
            return await _repository.GetAll<GetAllFeedbacksDTOs>().ConfigureAwait(false);
        }

        public async Task<GetAllFeedbacksDTOs> GetById(int id)
        {
            return await _repository.GetById<GetAllFeedbacksDTOs>(id).ConfigureAwait(false);
        }

        public async Task<bool> Create(CreateFeedbackDTO dto)
        {
            if (dto == null) return false;
            return await _repository.CreateAsync(dto).ConfigureAwait(false);
        }

        public async Task<bool> Update(int id, UpdateFeedbackDto dto)
        {
            if (dto == null) return false;
            var existing = await GetById(id).ConfigureAwait(false);
            if (existing == null) return false;

            var entity = _mapper.Map<Feedback>(dto);
            entity.Id = id;
            var result = await _repository.UpdateAsync(entity).ConfigureAwait(false);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var existing = await GetById(id).ConfigureAwait(false);
            if (existing == null) return false;

            await _repository.DeleteAsync(id).ConfigureAwait(false);
            return true;
        }
    }
}