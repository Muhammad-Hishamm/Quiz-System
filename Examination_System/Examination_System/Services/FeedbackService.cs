using AutoMapper;
using AutoMapper.QueryableExtensions;
using Examination_System.DTOs.Feedbacks;
using Examination_System.Models;
using Examination_System.Repositories;
using System.Collections.Generic;
using System.Linq;
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
            _repository = new GeneralRepository<Feedback>();
        }

        public IEnumerable<GetAllFeedbacksDTOs>? GetAll()
        {
            var query = _repository.GetAll()
                        .ProjectTo<GetAllFeedbacksDTOs>(_mapper.ConfigurationProvider);
            return query;
        }

        public GetAllFeedbacksDTOs GetById(int id)
        {
            var dto = _repository.GetById(id)
                      .ProjectTo<GetAllFeedbacksDTOs>(_mapper.ConfigurationProvider)
                      .FirstOrDefault();
            return dto;
        }

        public async Task<bool> Create(CreateFeedbackDTO dto)
        {
            if (dto == null) return false;
            var entity = _mapper.Map<Feedback>(dto);
            return await _repository.CreateAsync(entity).ConfigureAwait(false);
        }

        public async Task<bool> Update(int id, UpdateFeedbackDto dto)
        {
            if (dto == null) return false;
            if (this.GetById(id) == null) return false;

            var entity = _mapper.Map<Feedback>(dto);
            entity.Id = id;
            var result = await _repository.UpdateAsync(entity).ConfigureAwait(false);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var existing = this.GetById(id);
            if (existing == null) return false;

            await _repository.DeleteAsync(id).ConfigureAwait(false);
            return true;
        }
    }
}