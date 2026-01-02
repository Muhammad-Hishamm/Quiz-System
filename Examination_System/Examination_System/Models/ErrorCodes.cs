namespace Examination_System.Models
{
    public enum ErrorCode
    {
        NoError = 0,
        InvalidCouseID = 100,
        CourseNotFound = 101,
        InstructorNotFound = 201,
        StudentNotFound = 301,
        ExamNotFound = 401,
        QuestionNotFound = 501,
        BadRequest = 600

    }
}
