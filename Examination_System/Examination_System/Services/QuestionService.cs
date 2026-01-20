using AutoMapper;
using Examination_System.DTOs.Questions;
using Examination_System.Models;
using Examination_System.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Examination_System.Services
{
    public class QuestionService
    {
        private readonly GeneralRepository<Question> _generalRepository;
        private readonly IMapper _mapper;

        public QuestionService(IMapper mapper)
        {
            _mapper = mapper;
            _generalRepository = new GeneralRepository<Question>(_mapper);
        }

        public async Task<IEnumerable<GetAllQuestionsDTOs>> GetAll()
        {
            return await _generalRepository.GetAll<GetAllQuestionsDTOs>().ConfigureAwait(false);
        }

        public async Task<GetAllQuestionsDTOs> GetById(int id)
        {
            return await _generalRepository.GetById<GetAllQuestionsDTOs>(id).ConfigureAwait(false);
        }

        public async Task<bool> Create(CreateQuestionDTO dto)
        {
            if (dto == null) return false;
            return await _generalRepository.CreateAsync(dto).ConfigureAwait(false);
        }

        public async Task<bool> Update(int id, UpdateQuestionDto dto)
        {
            if (dto == null) return false;
            var existing = await GetById(id).ConfigureAwait(false);
            if (existing == null) return false;

            var entity = _mapper.Map<Question>(dto);
            entity.Id = id;
            var result = await _generalRepository.UpdateAsync(entity).ConfigureAwait(false);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var existing = await GetById(id).ConfigureAwait(false);
            if (existing == null) return false;

            await _generalRepository.DeleteAsync(id).ConfigureAwait(false);
            return true;
        }
    }
}