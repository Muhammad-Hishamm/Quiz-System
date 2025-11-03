using Examination_System.Data;
using Examination_System.Models;
using System.Threading.Tasks;

namespace Examination_System.Repositories
{
    public class CourseRepository
    {
        GeneralRepository<Course> _GeneralRepository;
        public CourseRepository()
        {
            _GeneralRepository = new GeneralRepository<Course>();
        }

        public IQueryable<Course> GetAll()
        {
            var courses =  _GeneralRepository.GetAll();     
            return courses;
        }

        public Task<Course> GetByIdAsync(int id)
        {
            var course =_GeneralRepository.GetByIDAsync(id);
            return course;
        }

        public async Task Add(Course course)
        {
            await _GeneralRepository.CreateAsync(course);
        }


        public async Task Update(Course course) 
        {
            await _GeneralRepository.UpdateAsync(course);
        }


        public async Task Delete(int id)
        {
          await  _GeneralRepository.DeleteAsync(id);
        }
    }
}
