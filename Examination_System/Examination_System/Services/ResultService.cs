using Examination_System.DTOs.Results;
using Examination_System.Models;
using Examination_System.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Examination_System.Services
{
    public class ResultService
    {
        private readonly GeneralRepository<Result> _generalRepository;

        public ResultService()
        {
            _generalRepository = new GeneralRepository<Result>();
        }

        public IQueryable<GetAllResultsDTOs>? GetAll()
        {
            return _generalRepository.GetAll<GetAllResultsDTOs>(r => new GetAllResultsDTOs
            {
                Id = r.Id,
                Score =r.Score,
                StudentId = r.StudentExam == null ? (int?)null : r.StudentExam.StudentId,
                ExamId = r.StudentExam == null ? (int?)null : r.StudentExam.ExamId
            });
        }

        public Task<GetAllResultsDTOs?> GetByIdAsync(int id)
        {
            return _generalRepository.GetByIdAsync<GetAllResultsDTOs>(id, r => new GetAllResultsDTOs
            {
                Id = r.Id,
                Score = r.Score,
                StudentId = r.StudentExam == null ? (int?)null : r.StudentExam.StudentId,
                ExamId = r.StudentExam == null ? (int?)null : r.StudentExam.ExamId
            });
        }

        public async Task<bool> Create(CreateResultDTO dto)
        {
            if (dto is null) return false;

            var result = new Result
            {
                Score = dto.Score
                // If you need to create/associate a StudentExam entity,
                // handle that in the service here (not shown).
            };

            await _generalRepository.CreateAsync(result).ConfigureAwait(false);
            return true;
        }

        public async Task<bool> Update(int id, UpdateResultDto updated)
        {
            if (updated == null) return false;

            var existing = await _generalRepository.GetByIdAsync(id).ConfigureAwait(false);
            if (existing == null) return false;

            existing.Score = updated.Score;
            // If you need to update StudentExam association, handle it explicitly here.

            await _generalRepository.UpdateAsync(existing).ConfigureAwait(false);
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var existing = await _generalRepository.GetByIdAsync(id).ConfigureAwait(false);
            if (existing == null) return false;

            await _generalRepository.DeleteAsync(id).ConfigureAwait(false);
            return true;
        }
    }
}