using Examination_System.Models;
using Examination_System.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Examination_System.Services
{
    public class InstructorService
    {
        GeneralRepository<Instructor> _instructorRepository;
        public InstructorService() 
        { 
            _instructorRepository = new GeneralRepository<Instructor>();
        }
       
        public IQueryable<Instructor> GetAll()
        {
            var instructors = _instructorRepository.GetAll();
            if (instructors == null)
            {
                return null;
            }
            return instructors;
        }

        public async Task<Instructor> GetById(int id)
        {
            var instructor = await _instructorRepository.GetByIDAsync(id);
            if (instructor == null)
            {
                return null;
            }
            return instructor;
        }

        public async Task<bool> Create(Instructor instrucor)
        {
            if (instrucor == null)
            {
                return false;
            }
          
            await _instructorRepository.CreateAsync(instrucor);
            return true;
        }

        public async Task<bool> Update(int id, Instructor updatedInstructor)
        {
            if (updatedInstructor == null)
                return false;

            // Load existing entity first
            var existing = await _instructorRepository.GetByIDAsync(id);
            if (existing == null)
                return false;

            // Map only the scalar fields you want to allow updating.
            existing.Name = updatedInstructor.Name;
            existing.Email = updatedInstructor.Email;
            existing.Department = updatedInstructor.Department;

            // Avoid replacing navigation collections unless intended.
            await _instructorRepository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            if(await GetById(id) == null)    return false;
            await _instructorRepository.DeleteAsync(id);
            return true;
        }
    }
}
