using AutoMapper;
using AutoMapper.QueryableExtensions;
using Examination_System.DTOs.Courses;
using Examination_System.Models;
using Examination_System.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Examination_System.Services
{
    public class CourseService
    {
        GeneralRepository<Course> _GeneralRepository;
        IMapper _mapper;
        public CourseService(IMapper mapper)
        {
            _mapper = mapper;
            _GeneralRepository = new GeneralRepository<Course>();
        }

        public IEnumerable<GetAllCoursesDTOs>? GetAll()
        {
            var query = _GeneralRepository.GetAll()
                        .ProjectTo<GetAllCoursesDTOs>(_mapper.ConfigurationProvider);
            return query;
        }

        public GetAllCoursesDTOs GetById(int id)
        {
            var courseDto =  _GeneralRepository.GetById(id)
                             .ProjectTo<GetAllCoursesDTOs>(_mapper.ConfigurationProvider)
                             .FirstOrDefault();
            return courseDto;
        }

        public async Task<bool> Create(CreateDTO courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);
            return await _GeneralRepository.CreateAsync(course).ConfigureAwait(false);
        }

        public async Task<bool> Update(int id, UpdateCourseDto updatedCourse)
        {
            var course = await _GeneralRepository.GetById(id).FirstOrDefaultAsync();
            if (updatedCourse == null)         return false;
            if (course == null)      return false;
            //course.Name = updatedCourse.Name;
            //course.Description = updatedCourse.Description;
            //course.CreditHours = updatedCourse.CreditHours;
            var course1 = _mapper.Map<Course>(updatedCourse);
            var result = await _GeneralRepository.UpdateAsync(course).ConfigureAwait(false);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var existing =  this.GetById(id);
            if (existing == null) return false;

            await _GeneralRepository.DeleteAsync(id).ConfigureAwait(false);
            return true;

        }
    }
}
