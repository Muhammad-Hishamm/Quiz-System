using Examination_System.DTOs.Instructors;

namespace Examination_System.ViewModels
{
    public class GetInstructorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }

        public GetInstructorViewModel ToViewModel(GetAllInstructorsDTOs dto)
        {
            if (dto == null) return null;
            return new GetInstructorViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Email = dto.Email,
                Department = dto.Department
            };
        }
    }
}