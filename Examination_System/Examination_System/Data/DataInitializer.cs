using Examination_System.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Examination_System.Data.Enums;

namespace Examination_System.Data
{
    /// <summary>
    /// Simple data initializer/seed helper for development.
    /// Call DataInitializer.Initialize(context) once (for example from Program.cs) to populate sample data.
    /// </summary>
    public static class DataInitializer
    {
        public static void Initialize(Context context)
        {
            // avoid seeding twice
            if (context.Instructors.Any() || context.Students.Any() || context.Courses.Any())
            {
                return;
            }

            var now = DateTime.UtcNow;
            var rnd = new Random(42);

            // 1. Instructors (10)
            var instructors = Enumerable.Range(1, 10).Select(i => new Instructor
            {
                Name = $"Instructor {i}",
                Email = $"instructor{i}@school.edu",
                Department = i % 2 == 0 ? "Computer Science" : "Mathematics",
                CreatedAt = now
            }).ToList();
            context.Instructors.AddRange(instructors);
            context.SaveChanges();

            // 2. Students (10)
            var students = Enumerable.Range(1, 10).Select(i => new Student
            {
                Name = $"Student {i}",
                Email = $"student{i}@school.edu",
                CreatedAt = now
            }).ToList();
            context.Students.AddRange(students);
            context.SaveChanges();

            // 3. Courses (10) - assign instructors round-robin
            var courses = Enumerable.Range(1, 10).Select(i =>
            {
                var instr = instructors[(i - 1) % instructors.Count];
                return new Course
                {
                    Name = $"Course {i}",
                    Description = $"Description for course {i}",
                    CreditHours = 2 + (i % 4),
                    InstructorId = instr.Id,
                    CreatedAt = now
                };
            }).ToList();
            context.Courses.AddRange(courses);
            context.SaveChanges();

            // 4. Exams (10) - assign to courses and instructors
            var exams = Enumerable.Range(1, 10).Select(i =>
            {
                var course = courses[(i - 1) % courses.Count];
                var instr = instructors[(i - 1) % instructors.Count];
                return new Exam
                {
                    Title = $"Exam {i} - {course.Name}",
                    Type = (i % 2 == 0) ? ExamType.Final : ExamType.Quiz,
                    InstructorId = instr.Id,
                    CourseId = course.Id,
                    NumberOfQuestions = 5 + (i % 3),
                    CreatedAt = now
                };
            }).ToList();
            context.Exams.AddRange(exams);
            context.SaveChanges();

            // 5. Questions (10) - assign instructors
            var levels = new[] { QuestionLevel.Simple, QuestionLevel.Medium, QuestionLevel.Hard };
            var questions = Enumerable.Range(1, 10).Select(i => new Question
            {
                QuestionBody = $"What is sample question number {i}?",
                Level = levels[i % levels.Length],
                InstructorId = instructors[(i - 1) % instructors.Count].Id,
                CreatedAt = now
            }).ToList();
            context.Questions.AddRange(questions);
            context.SaveChanges();

            // 6. Choices (30) - create multiple choices and associate with instructors optionally
            var choices = new List<Choice>();
            for (int i = 1; i <= 30; i++)
            {
                choices.Add(new Choice
                {
                    ChoiceBody = $"Choice body {i}",
                    InstructorId = (i % 3 == 0) ? instructors[(i - 1) % instructors.Count].Id : (int?)null,
                    CreatedAt = now
                });
            }
            context.Choices.AddRange(choices);
            context.SaveChanges();

            // 7. QuestionChoice (map each question to 4 choices)
            var questionChoices = new List<QuestionChoice>();
            for (int q = 0; q < questions.Count; q++)
            {
                // pick 4 distinct choices per question
                var start = (q * 3) % choices.Count;
                for (int j = 0; j < 4; j++)
                {
                    var c = choices[(start + j) % choices.Count];
                    questionChoices.Add(new QuestionChoice
                    {
                        QuestionId = questions[q].Id,
                        ChoiceId = c.Id,
                        CreatedAt = now
                    });
                }
            }
            context.AddRange(questionChoices);
            context.SaveChanges();

            // 8. ExamQuestion (link each exam to 5 questions, reusing questions)
            var examQuestions = new List<ExamQuestion>();
            for (int e = 0; e < exams.Count; e++)
            {
                for (int j = 0; j < 5; j++)
                {
                    var q = questions[(e + j) % questions.Count];
                    examQuestions.Add(new ExamQuestion
                    {
                        ExamId = exams[e].Id,
                        QuestionId = q.Id,
                        CreatedAt = now
                    });
                }
            }
            context.AddRange(examQuestions);
            context.SaveChanges();

            // 9. StudentCourse (enroll students in courses) - ensure at least 10 entries
            var studentCourses = new List<StudentCourse>();
            for (int s = 0; s < students.Count; s++)
            {
                // enroll each student in 2 courses
                for (int k = 0; k < 2; k++)
                {
                    var course = courses[(s + k) % courses.Count];
                    studentCourses.Add(new StudentCourse
                    {
                        StudentId = students[s].Id,
                        CourseId = course.Id,
                        CreatedAt = now
                    });
                }
            }
            context.AddRange(studentCourses);
            context.SaveChanges();

            // 10. StudentExam (create at least 10) - each student takes an exam
            var studentExams = new List<StudentExam>();
            for (int s = 0; s < students.Count; s++)
            {
                var exam = exams[s % exams.Count];
                var score = Math.Round(rnd.NextDouble() * 100.0, 2);
                studentExams.Add(new StudentExam
                {
                    StudentId = students[s].Id,
                    ExamId = exam.Id,
                    Score = score,
                    IsSubmitted = true,
                    SubmissionTime = now.AddMinutes(-rnd.Next(0, 120)),
                    CreatedAt = now
                });
            }
            context.AddRange(studentExams);
            context.SaveChanges();

            // 11. Answers (for each studentExam fill answers for the exam's questions)
            var answers = new List<Answer>();
            foreach (var se in studentExams)
            {
                // find questions for this exam
                var eqs = context.ExamQuestions.Where(x => x.ExamId == se.ExamId).Select(x => x.QuestionId).ToList();
                foreach (var qid in eqs)
                {
                    answers.Add(new Answer
                    {
                        AnswerBody = $"Answer for question {qid} by studentExam {se.Id}",
                        StudentExamId = se.Id,
                        QuestionId = qid,
                        CreatedAt = now
                    });
                }
            }
            context.AddRange(answers);
            context.SaveChanges();

            // 12. Results (create a result for each StudentExam)
            var results = new List<Result>();
            foreach (var se in studentExams)
            {
                var studentId = se.StudentId;
                var score = se.Score; // reuse StudentExam.Score
                var result = new Result
                {
                    StudentId = studentId,
                    StudentExamId = se.Id,
                    Score = score,
                    CreatedAt = now
                };
                results.Add(result);
            }
            context.AddRange(results);
            context.SaveChanges();

            // 13. Feedbacks (one feedback per result) - Feedback has ResultId FK
            var feedbacks = new List<Feedback>();
            for (int i = 0; i < results.Count; i++)
            {
                var res = results[i];
                var se = studentExams.First(x => x.Id == res.StudentExamId);
                feedbacks.Add(new Feedback
                {
                    ResultId = res.Id,
                    ExamId = se.ExamId,
                    StudentId = res.StudentId,
                    Rating = 3 + (i % 3),
                    Comment = $"Feedback comment #{i + 1}",
                    CreatedAt = now
                });
            }
            context.AddRange(feedbacks);
            context.SaveChanges();

            // At this point we have at least 10 records for each of the project's DbSets:
            // Instructors (10), Students (10), Courses (10), Exams (10), Questions (10),
            // Choices (30), QuestionChoices (40), ExamQuestions (50), StudentCourses (20),
            // StudentExams (10), Answers (multiple), Results (10), Feedbacks (10), InstructorStudents (10).
            
        }
    }
}