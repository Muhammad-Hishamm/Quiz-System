using Examination_System.DTOs.Students;

namespace Examination_System.ViewModels
{
    public class GetStudentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public GetStudentViewModel ToViewModel(GetAllStudentsDTOs dto)
        {
            if (dto == null) return null;
            return new GetStudentViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Email = dto.Email
            };
        }
    }
}