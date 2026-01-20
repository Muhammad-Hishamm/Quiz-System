using AutoMapper;
using AutoMapper.QueryableExtensions;
using Examination_System.DTOs.Courses;
using Examination_System.Models;
using Examination_System.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Examination_System.Services
{
    public class CourseService
    {
        GeneralRepository<Course> _GeneralRepository;
        IMapper _mapper;
        public CourseService(IMapper mapper)
        {
            _mapper = mapper;
            _GeneralRepository = new GeneralRepository<Course>(_mapper);
        }

        public async Task<IEnumerable<GetAllCoursesDTOs>> GetAll(Expression<Func<Course, bool>>? filter = null)
        {
            return await _GeneralRepository.GetAll<GetAllCoursesDTOs>(filter);
        }

        public async Task< GetAllCoursesDTOs> GetById(int id)
        {
            var courseDto = await _GeneralRepository.GetById<GetAllCoursesDTOs>(id);
            return courseDto;
        }

        public async Task<bool> Create(CreateDTO courseDTO)
        {
            return await _GeneralRepository.CreateAsync(courseDTO).ConfigureAwait(false);
        }

        public async Task<bool> Update(int id, UpdateCourseDto updatedCourse)
        {
            if (updatedCourse == null) return false;// to check if the dto itself is a null or not

            var courseEntity = await _GeneralRepository.GetById<Course>(id).ConfigureAwait(false);// get the course that we want to update 
            if (courseEntity == null) return false;  // to check if there is no course with this id

            _mapper.Map(updatedCourse, courseEntity);// edit the dto with the entity

            var result = await _GeneralRepository.UpdateAsync(courseEntity).ConfigureAwait(false);// update the entity
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var existing =  this.GetById(id);
            if (existing == null) return false;

            await _GeneralRepository.DeleteAsync(id).ConfigureAwait(false);
            return true;

        }
        public async Task<bool> IsExist(int id)
        {
            var course = await _GeneralRepository.GetById<GetAllCoursesDTOs>(id);
            return course != null;
        }

    }
}
