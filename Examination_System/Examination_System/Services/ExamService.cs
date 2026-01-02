using AutoMapper;
using AutoMapper.QueryableExtensions;
using Examination_System.DTOs.Exams;
using Examination_System.Models;
using Examination_System.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination_System.Services
{
    public class ExamService
    {
        private readonly GeneralRepository<Exam> _generalRepository;
        private readonly IMapper _mapper;

        public ExamService(IMapper mapper)
        {
            _mapper = mapper;
            _generalRepository = new GeneralRepository<Exam>();
        }

        public IEnumerable<GetAllExamsDTOs>? GetAll()
        {
            var query = _generalRepository.GetAll()
                        .ProjectTo<GetAllExamsDTOs>(_mapper.ConfigurationProvider);
            return query;
        }

        public GetAllExamsDTOs GetById(int id)
        {
            var dto = _generalRepository.GetById(id)
                      .ProjectTo<GetAllExamsDTOs>(_mapper.ConfigurationProvider)
                      .FirstOrDefault();
            return dto;
        }

        public async Task<bool> Create(CreateExamDTO examDto)
        {
            if (examDto == null) return false;
            var exam = _mapper.Map<Exam>(examDto);
            return await _generalRepository.CreateAsync(exam).ConfigureAwait(false);
        }

        public async Task<bool> Update(int id, UpdateExamDto updatedExam)
        {
            if (updatedExam == null) return false;
            if (this.GetById(id) == null) return false;

            var exam = _mapper.Map<Exam>(updatedExam);
            exam.Id = id;
            var result = await _generalRepository.UpdateAsync(exam).ConfigureAwait(false);
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