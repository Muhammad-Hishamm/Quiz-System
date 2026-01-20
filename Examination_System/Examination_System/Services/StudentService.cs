using AutoMapper;
using AutoMapper.QueryableExtensions;
using Examination_System.DTOs.Courses;
using Examination_System.DTOs.Students;
using Examination_System.Models;
using Examination_System.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Examination_System.Services
{
    public class StudentService
    {
        private readonly GeneralRepository<Student> _generalRepository;
        private readonly IMapper _mapper;

        public StudentService(IMapper mapper)
        {
            _mapper = mapper;
            _generalRepository = new GeneralRepository<Student>(_mapper);
        }

        public async Task<IEnumerable<GetAllStudentsDTOs>?> GetAll(Expression<Func<Student, bool>>? filter = null)
        {
            return await _generalRepository.GetAll<GetAllStudentsDTOs>(filter);
        }

        public async Task<GetAllStudentsDTOs> GetById(int id)
        {
            var Studentdto = await _generalRepository.GetById<GetAllStudentsDTOs>(id);
            return Studentdto;
        }

        public async Task<bool> Create(CreateStudentDTO studentDto)
        {
            var student = _mapper.Map<Student>(studentDto);
            return await _generalRepository.CreateAsync(student).ConfigureAwait(false);
        }

        public async Task<bool> Update(int id, UpdateStudentDto updatedStudent)
        {
            if (updatedStudent == null) return false;
            if (this.GetById(id) == null) return false;

            var student = _mapper.Map<Student>(updatedStudent);
            student.Id = id;
            var result = await _generalRepository.UpdateAsync(student).ConfigureAwait(false);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var existing = this.GetById(id);
            if (existing == null) return false;

            await _generalRepository.DeleteAsync(id).ConfigureAwait(false);
            return true;
        }
    }
}