using Examination_System.ViewModels.Student;

namespace Examination_System.DTOs.Students
{
    public class UpdateStudentDto
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public UpdateStudentDto ToDTO(UpdateStudentViewModel vm)
        {
            if (vm == null) return null;
            return new UpdateStudentDto
            {
                Name = vm.Name,
                Email = vm.Email
            };
        }
    }
}