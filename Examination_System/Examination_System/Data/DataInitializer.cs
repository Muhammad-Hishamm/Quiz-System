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
            // Ensure database exists
            context.Database.EnsureCreated();

            // Avoid seeding more than once
            if (context.Instructors.Any()) return;

            // Instructors
            var instructors = new List<Instructor>
            {
                new Instructor { Name = "Dr. Alice Smith", Email = "alice.smith@university.edu", Department = "Computer Science" },
                new Instructor { Name = "Dr. Bob Johnson", Email = "bob.johnson@university.edu", Department = "Mathematics" }
            };
            context.Instructors.AddRange(instructors);
            context.SaveChanges();

            // Courses
            var courses = new List<Course>
            {
                new Course { Name = "Data Structures", Description = "Intro to data structures", CreditHours = 3, InstructorId = instructors[0].Id },
                new Course { Name = "Algorithms", Description = "Design and analysis of algorithms", CreditHours = 4, InstructorId = instructors[0].Id },
                new Course { Name = "Calculus I", Description = "Differential Calculus", CreditHours = 3, InstructorId = instructors[1].Id }
            };
            context.Courses.AddRange(courses);
            context.SaveChanges();

            // Exams
            var exams = new List<Exam>
            {
                new Exam { Title = "DS - Quiz 1", Type = ExamType.Quiz, NumberOfQuestions = 3, CourseId = courses[0].Id },
                new Exam { Title = "Algorithms - Final", Type = ExamType.Final, NumberOfQuestions = 4, CourseId = courses[1].Id }
            };
            context.Exams.AddRange(exams);
            context.SaveChanges();

            // Questions
            var questions = new List<Question>
            {
                new Question { Level = QuestionLevel.Simple, QuestionBody = "What is the time complexity of binary search?", InstructorId = instructors[0].Id },
                new Question { Level = QuestionLevel.Medium, QuestionBody = "Describe a balanced binary tree.", InstructorId = instructors[0].Id },
                new Question { Level = QuestionLevel.Simple, QuestionBody = "What is the derivative of x^2?", InstructorId = instructors[1].Id }
            };
            context.Questions.AddRange(questions);
            context.SaveChanges();

            // Choices
            var choices = new List<Choice>
            {
                new Choice { ChoiceBody = "O(log n)", IsCorrect = true, QuestionId = questions[0].Id },
                new Choice { ChoiceBody = "O(n)", IsCorrect = false, QuestionId = questions[0].Id },
                new Choice { ChoiceBody = "A tree with height balanced", IsCorrect = true, QuestionId = questions[1].Id },
                new Choice { ChoiceBody = "A tree with equal number of nodes", IsCorrect = false, QuestionId = questions[1].Id },
                new Choice { ChoiceBody = "2x", IsCorrect = true, QuestionId = questions[2].Id },
                new Choice { ChoiceBody = "x^2", IsCorrect = false, QuestionId = questions[2].Id }
            };
            context.Choices.AddRange(choices);
            context.SaveChanges();

            // Link questions to exams (ExamQuestion many-to-many)
            var examQuestions = new List<ExamQuestion>
            {
                new ExamQuestion { ExamId = exams[0].Id, QuestionId = questions[0].Id },
                new ExamQuestion { ExamId = exams[0].Id, QuestionId = questions[1].Id },
                new ExamQuestion { ExamId = exams[1].Id, QuestionId = questions[1].Id },
                new ExamQuestion { ExamId = exams[1].Id, QuestionId = questions[2].Id }
            };
            context.ExamQuestions.AddRange(examQuestions);
            context.SaveChanges();

            // Students
            var students = new List<Student>
            {
                new Student { Name = "John Doe", Email = "john.doe@student.edu" },
                new Student { Name = "Jane Roe", Email = "jane.roe@student.edu" }
            };
            context.Students.AddRange(students);
            context.SaveChanges();

            // StudentCourse (many-to-many)
            var studentCourses = new List<StudentCourse>
            {
                new StudentCourse { StudentId = students[0].Id, CourseId = courses[0].Id },
                new StudentCourse { StudentId = students[0].Id, CourseId = courses[1].Id },
                new StudentCourse { StudentId = students[1].Id, CourseId = courses[0].Id }
            };
            context.StudentCourses.AddRange(studentCourses);
            context.SaveChanges();

            // StudentExam entries (student attempts)
            var studentExams = new List<StudentExam>
            {
                new StudentExam { StudentId = students[0].Id, ExamId = exams[0].Id, Score = 85.0, IsSubmitted = true, SubmissionTime = DateTime.UtcNow },
                new StudentExam { StudentId = students[1].Id, ExamId = exams[0].Id, Score = 92.5, IsSubmitted = true, SubmissionTime = DateTime.UtcNow }
            };
            context.StudentExams.AddRange(studentExams);
            context.SaveChanges();

            // Results (simple standalone results)
            //var results = new List<Result>
            //{
            //    new Result { Score = 85.0 },
            //    new Result { Score = 92.5 }
            //};
            //context.Results.AddRange(results);
            //context.SaveChanges();

            //// Feedbacks linked to results (if you later wire Result ⇄ StudentExam, adjust accordingly)
            //var feedbacks = new List<Feedback>
            //{
            //    new Feedback { Rating = 4, Comments = "Good exam.", ResultId = results[0].Id },
            //    new Feedback { Rating = 5, Comments = "Well designed questions.", ResultId = results[1].Id }
            //};
            //context.Feedbacks.AddRange(feedbacks);
            //context.SaveChanges();
        }
    }
}