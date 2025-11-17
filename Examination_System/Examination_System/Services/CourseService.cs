using Examination_System.Models;
using Examination_System.Repositories;

namespace Examination_System.Services
{
    public class CourseService
    {
        GeneralRepository<Course> _courseRepository;
        public CourseService()
        {
            _courseRepository = new GeneralRepository<Course>();
        }

        public IQueryable<Course> GetAll()
        {
            var courses = _courseRepository.GetAll();
            if (courses == null)
            {
                return null;
            }
            return courses;
        }

        public async Task<Course> GetById(int id)
        {
            var course = await _courseRepository.GetByIDAsync(id);
            if (course == null)
            {
                return null;
            }
            return course;
        }

        public async Task<bool> Create(Course course)
        {
            if (course == null)
            {
                return false;
            }

            await _courseRepository.CreateAsync(course);
            return true;
        }

        public async Task<bool> Update(int id, Course updatedCourse)
        {
            if (updatedCourse == null)
                return false;

            // Load existing entity first
            var existing = await _courseRepository.GetByIDAsync(id);
            if (existing == null)
                return false;

            // Map only the scalar fields you want to allow updating.
            existing.Name = updatedCourse.Name;

            // Avoid replacing navigation collections unless intended.
            await _courseRepository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if (await GetById(id) == null) return false;
            await _courseRepository.DeleteAsync(id);
            return true;
        }
    }
}
