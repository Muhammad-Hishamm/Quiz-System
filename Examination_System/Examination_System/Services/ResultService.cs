using Examination_System.Models;
using Examination_System.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Examination_System.Services
{
    public class ResultService
    {
        private readonly GeneralRepository<Result> _resultRepository;

        public ResultService()
        {
            _resultRepository = new GeneralRepository<Result>();
        }

        public IQueryable<Result> GetAll()
        {
            return _resultRepository.GetAll();
        }

        public async Task<Result> GetById(int id)
        {
            return await _resultRepository.GetByIDAsync(id);
        }

        public async Task<bool> Create(Result result)
        {
            if (result == null) return false;
            await _resultRepository.CreateAsync(result);
            return true;
        }

        public async Task<bool> Update(int id, Result updatedResult)
        {
            if (updatedResult == null) return false;

            var existing = await _resultRepository.GetByIDAsync(id);
            if (existing == null) return false;

            existing.Score = updatedResult.Score;
            // keep relational keys/navigation unchanged unless needed

            await _resultRepository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var existing = await GetById(id);
            if (existing == null) return false;
            await _resultRepository.DeleteAsync(id);
            return true;
        }
    }
}