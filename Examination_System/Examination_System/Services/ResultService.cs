using AutoMapper;
using AutoMapper.QueryableExtensions;
using Examination_System.DTOs.Results;
using Examination_System.Models;
using Examination_System.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination_System.Services
{
    public class ResultService
    {
        private readonly GeneralRepository<Result> _generalRepository;
        private readonly IMapper _mapper;

        public ResultService(IMapper mapper)
        {
            _mapper = mapper;
            _generalRepository = new GeneralRepository<Result>();
        }

        public IEnumerable<GetAllResultsDTOs>? GetAll()
        {
            var query = _generalRepository.GetAll()
                        .ProjectTo<GetAllResultsDTOs>(_mapper.ConfigurationProvider);
            return query;
        }

        public GetAllResultsDTOs GetById(int id)
        {
            var dto = _generalRepository.GetById(id)
                      .ProjectTo<GetAllResultsDTOs>(_mapper.ConfigurationProvider)
                      .FirstOrDefault();
            return dto;
        }

        public async Task<bool> Create(CreateResultDTO dto)
        {
            if (dto == null) return false;
            var entity = _mapper.Map<Result>(dto);
            return await _generalRepository.CreateAsync(entity).ConfigureAwait(false);
        }

        public async Task<bool> Update(int id, UpdateResultDto dto)
        {
            if (dto == null) return false;
            if (this.GetById(id) == null) return false;

            var entity = _mapper.Map<Result>(dto);
            entity.Id = id;
            var result = await _generalRepository.UpdateAsync(entity).ConfigureAwait(false);
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