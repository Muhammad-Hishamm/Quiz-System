using Examination_System.ViewModels.Student;

namespace Examination_System.DTOs.Students
{
    public class CreateStudentDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public CreateStudentDTO ToDTO(CreateStudentViewModel vm)
        {
            if (vm == null) return null;
            return new CreateStudentDTO
            {
                Name = vm.Name,
                Email = vm.Email
            };
        }
    }
}