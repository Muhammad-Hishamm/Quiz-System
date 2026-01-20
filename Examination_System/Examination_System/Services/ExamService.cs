using AutoMapper;
using Examination_System.DTOs.Exams;
using Examination_System.Models;
using Examination_System.Repositories;
using System.Collections.Generic;
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
            _generalRepository = new GeneralRepository<Exam>(_mapper);
        }

        public async Task<IEnumerable<GetAllExamsDTOs>> GetAll()
        {
            return await _generalRepository.GetAll<GetAllExamsDTOs>().ConfigureAwait(false);
        }

        public async Task<GetAllExamsDTOs> GetById(int id)
        {
            return await _generalRepository.GetById<GetAllExamsDTOs>(id).ConfigureAwait(false);
        }

        public async Task<bool> Create(CreateExamDTO examDto)
        {
            if (examDto == null) return false;
            return await _generalRepository.CreateAsync(examDto).ConfigureAwait(false);
        }

        public async Task<bool> Update(int id, UpdateExamDto updatedExam)
        {
            if (updatedExam == null) return false;
            var existing = await GetById(id).ConfigureAwait(false);
            if (existing == null) return false;

            var exam = _mapper.Map<Exam>(updatedExam);
            exam.Id = id;
            var result = await _generalRepository.UpdateAsync(exam).ConfigureAwait(false);
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