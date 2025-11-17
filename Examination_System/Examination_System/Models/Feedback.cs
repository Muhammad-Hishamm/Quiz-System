namespace Examination_System.Models
{
    public class Feedback : BaseModel
    {
        public int Rating { get; set; }
        public string Comments { get; set; }
        public int ResultId { get; set; }
        public Result? Result { get; set; }


    }
}
