namespace Examination_System.Models
{
    public class QuestionChoice: BaseModel
    {
        public int QuestionId { get; set; }
        public Question? Question { get; set; }
        public int ChoiceId { get; set; }
        public Choice? Choice { get; set; }

    }
}
