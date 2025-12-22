using Examination_System.DTOs.Courses;
using Examination_System.Models;
using Examination_System.Repositories;

namespace Examination_System.Services
{
    public class CourseService
    {
        GeneralRepository<Course> _GeneralRepository;
        public CourseService()
        {
            _GeneralRepository = new GeneralRepository<Course>();
        }

        public IQueryable<GetAllCoursesDTOs>? GetAll()
        {
            return _GeneralRepository.GetAll<GetAllCoursesDTOs>(c => new GetAllCoursesDTOs
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                CreditHours = c.CreditHours
            });
        }

        public Task<GetAllCoursesDTOs?> GetByIdAsync(int id)
        {
            return _GeneralRepository.GetByIdAsync<GetAllCoursesDTOs>(id, c => new GetAllCoursesDTOs
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                CreditHours = c.CreditHours
            });
        }

        public async Task<bool> Create(CreateDTO courseDto)
        {
            if (courseDto is null) return false;
            var course = new Course
            {
                Name = courseDto.Name,
                Description = courseDto.Description,
                CreditHours = courseDto.CreditHours
            };

            await _GeneralRepository.CreateAsync(course).ConfigureAwait(false);
            return true;
        }

        public async Task<bool> Update(int id, UpdateCourseDto updatedCourse)
        {
            if (updatedCourse == null)         return false;
            var existing = await _GeneralRepository.GetByIdAsync(id);
            if (existing == null)              return false;

            existing.Id = id;
            existing.Name = updatedCourse.Name;
            existing.Description = updatedCourse.Description;
            existing.CreditHours = updatedCourse.CreditHours;

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
