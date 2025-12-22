using Examination_System.DTOs.Exams;
using Examination_System.Models;
using Examination_System.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Examination_System.Services
{
    public class ExamService
    {
        GeneralRepository<Exam> _GeneralRepository;
        public ExamService()
        {
            _GeneralRepository = new GeneralRepository<Exam>();
        }

        public IQueryable<GetAllExamsDTOs>? GetAll()
        {
            return _GeneralRepository.GetAll<GetAllExamsDTOs>(e => new GetAllExamsDTOs
            {
                Id = e.Id,
                Title = e.Title,
                Type = e.Type,
                NumberOfQuestions = e.NumberOfQuestions,
                CourseId = e.CourseId
            });
        }

        public Task<GetAllExamsDTOs?> GetByIdAsync(int id)
        {
            return _GeneralRepository.GetByIdAsync<GetAllExamsDTOs>(id, e => new GetAllExamsDTOs
            {
                Id = e.Id,
                Title = e.Title,
                Type = e.Type,
                NumberOfQuestions = e.NumberOfQuestions,
                CourseId = e.CourseId
            });
        }

        public async Task<bool> Create(CreateExamDTO examDto)
        {
            if (examDto is null) return false;
            var exam = new Exam
            {
                Title = examDto.Title,
                Type = examDto.Type,
                NumberOfQuestions = examDto.NumberOfQuestions,
                CourseId = examDto.CourseId
            };

            await _GeneralRepository.CreateAsync(exam).ConfigureAwait(false);
            return true;
        }

        public async Task<bool> Update(int id, UpdateExamDto updatedExam)
        {
            if (updatedExam == null) return false;
            var existing = await _GeneralRepository.GetByIdAsync(id).ConfigureAwait(false);
            if (existing == null) return false;

            existing.Id = id;
            existing.Title = updatedExam.Title;
            existing.Type = updatedExam.Type;
            existing.NumberOfQuestions = updatedExam.NumberOfQuestions;
            existing.CourseId = updatedExam.CourseId;

            await _GeneralRepository.UpdateAsync(existing).ConfigureAwait(false);
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var existing = await this.GetByIdAsync(id).ConfigureAwait(false);
            if (existing == null) return false;

            await _GeneralRepository.DeleteAsync(id).ConfigureAwait(false);
            return true;
        }
    }
}