using AutoMapper;
using Examination_System.DTOs.Results;
using Examination_System.Models;
using Examination_System.Repositories;
using System.Collections.Generic;
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
            _generalRepository = new GeneralRepository<Result>(_mapper);
        }

        public async Task<IEnumerable<GetAllResultsDTOs>> GetAll()
        {
            return await _generalRepository.GetAll<GetAllResultsDTOs>().ConfigureAwait(false);
        }

        public async Task<GetAllResultsDTOs> GetById(int id)
        {
            return await _generalRepository.GetById<GetAllResultsDTOs>(id).ConfigureAwait(false);
        }

        public async Task<bool> Create(CreateResultDTO dto)
        {
            if (dto == null) return false;
            return await _generalRepository.CreateAsync(dto).ConfigureAwait(false);
        }

        public async Task<bool> Update(int id, UpdateResultDto dto)
        {
            if (dto == null) return false;
            var existing = await GetById(id).ConfigureAwait(false);
            if (existing == null) return false;

            var entity = _mapper.Map<Result>(dto);
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