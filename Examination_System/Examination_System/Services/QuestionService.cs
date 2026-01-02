using AutoMapper;
using AutoMapper.QueryableExtensions;
using Examination_System.DTOs.Questions;
using Examination_System.Models;
using Examination_System.Repositories;
using System.Collections.Generic;
using System.Linq;
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
            _generalRepository = new GeneralRepository<Question>();
        }

        public IEnumerable<GetAllQuestionsDTOs>? GetAll()
        {
            var query = _generalRepository.GetAll()
                        .ProjectTo<GetAllQuestionsDTOs>(_mapper.ConfigurationProvider);
            return query;
        }

        public GetAllQuestionsDTOs GetById(int id)
        {
            var dto = _generalRepository.GetById(id)
                      .ProjectTo<GetAllQuestionsDTOs>(_mapper.ConfigurationProvider)
                      .FirstOrDefault();
            return dto;
        }

        public async Task<bool> Create(CreateQuestionDTO dto)
        {
            if (dto == null) return false;
            var question = _mapper.Map<Question>(dto);
            return await _generalRepository.CreateAsync(question).ConfigureAwait(false);
        }

        public async Task<bool> Update(int id, UpdateQuestionDto dto)
        {
            if (dto == null) return false;
            if (this.GetById(id) == null) return false;

            var question = _mapper.Map<Question>(dto);
            question.Id = id;
            var result = await _generalRepository.UpdateAsync(question).ConfigureAwait(false);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var existing = this.GetById(id);
            if (existing == null) return false;

            await _generalRepository.DeleteAsync(id).ConfigureAwait(false);
            return true;
        }
    }
}