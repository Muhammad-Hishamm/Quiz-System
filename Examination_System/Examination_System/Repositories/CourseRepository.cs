//using Examination_System.Data;
//using Examination_System.DTOs.Courses;
//using Examination_System.Models;
//using System.Threading.Tasks;

//namespace Examination_System.Repositories
//{
//    public class CourseRepository
//    {
//        GeneralRepository<Course> _GeneralRepository;
//        public CourseRepository()
//        {
//            _GeneralRepository = new GeneralRepository<Course>();
//        }

//        public IQueryable<GetAllCoursesDTOs> GetAll()
//        {
//            var coursesQuery = _GeneralRepository.GetAll<GetAllCoursesDTOs>(c => new GetAllCoursesDTOs
//            {
//                Name = c.Name,
//                Description = c.Description,
//                CreditHours = c.CreditHours,
//            });
//            return coursesQuery;
//        }

//        public Task<GetAllCoursesDTOs> GetByIdAsync(int id)
//        {
//            var course =_GeneralRepository.GetByIdAsync<GetAllCoursesDTOs> (id,c => new GetAllCoursesDTOs
//            {
//                Name = c.Name,
//                Description = c.Description,
//                CreditHours = c.CreditHours,
//            });
//            return course;
//        }

//        public async Task Add(Course course)
//        {
//            await _GeneralRepository.CreateAsync(course);
//        }


//        public async Task Update(Course course) 
//        {
//            await _GeneralRepository.UpdateAsync(course);
//        }


//        public async Task Delete(int id)
//        {
//          await  _GeneralRepository.DeleteAsync(id);
//        }
//    }
//}
