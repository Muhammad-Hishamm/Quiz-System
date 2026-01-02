using Examination_System.ViewModels.Instructor;

namespace Examination_System.DTOs.Instructors
{
    public class CreateInstructorDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }

        public CreateInstructorDTO ToDTO(CreateInstructorViewModel vm)
        {
            if (vm == null) return null;
            return new CreateInstructorDTO
            {
                Name = vm.Name,
                Email = vm.Email,
                Department = vm.Department
            };
        }
    }
}