using Examination_System.ViewModels.Instructor;

namespace Examination_System.DTOs.Instructors
{
    public class UpdateInstructorDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }

        public UpdateInstructorDto ToDTO(UpdateInstructorViewModel vm)
        {
            if (vm == null) return null;
            return new UpdateInstructorDto
            {
                Name = vm.Name,
                Email = vm.Email,
                Department = vm.Department
            };
        }
    }
}