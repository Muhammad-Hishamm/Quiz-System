using Examination_System.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Examination_System.Data
{
    public class Context : DbContext
    {
        //public Context(DbContextOptions<Context> options) : base(options)
        //{
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=MUHAMMAD-HISHAM\\SQLEXPRESS;Database=ExamSystem;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=True")
                .EnableSensitiveDataLogging()
                .LogTo(log => Debug.WriteLine(log), LogLevel.Information)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ExamQuestion>()
                .HasKey(eq => new { eq.ExamId, eq.QuestionId });

            modelBuilder.Entity<InstructorStudent>()
                .HasKey(IS => new { IS.InstructorId, IS.StudentId });

            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudentExam>()
                .HasOne(se => se.Student)
                .WithMany(s => s.StudentExams)
                .HasForeignKey(se => se.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudentExam>()
                .HasOne(se => se.Exam)
                .WithMany(e => e.StudentExams)
                .HasForeignKey(se => se.ExamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ExamQuestion>()
                .HasOne(eq => eq.Exam)
                .WithMany(e => e.ExamQuestions)
                .HasForeignKey(eq => eq.ExamId)
                .OnDelete(DeleteBehavior.Restrict); // prevents cascade path

            modelBuilder.Entity<ExamQuestion>()
                .HasOne(eq => eq.Question)
                .WithMany(q => q.ExamQuestions)
                .HasForeignKey(eq => eq.QuestionId)
                .OnDelete(DeleteBehavior.Restrict); // prevents cascade path


            modelBuilder.Entity<Question>()
                .HasMany(q => q.QuestionChoices)
                .WithOne(c => c.Question)
                .HasForeignKey(c => c.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Instructor>()
                .HasMany(i => i.Choices)
                .WithOne(c => c.Instructor)
                .HasForeignKey(c => c.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Exams)
                .WithOne(q => q.Course)
                .HasForeignKey(q => q.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Instructor>()
                .HasMany(i => i.Courses)
                .WithOne(c => c.Instructor)
                .HasForeignKey(c => c.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Instructor>()
                .HasMany(i => i.Questions)
                .WithOne(q => q.Instructor)
                .HasForeignKey(q => q.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Result>()
                .HasOne(r => r.Student)
                .WithMany(s => s.Results)
                .HasForeignKey(r => r.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Student)
                .WithMany(s => s.Feedbacks)
                .HasForeignKey(f => f.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Result>()
                .HasOne(r => r.Feedback)
                .WithOne(f => f.Result)
                .HasForeignKey<Feedback>(f => f.ResultId);
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Exam> Exams { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<StudentExam> StudentExams { get; set; }
        public DbSet<ExamQuestion> ExamQuestions { get; set; }
        public DbSet<InstructorStudent> InstructorStudents { get; set; }
        public DbSet<Answer> Answers { get; set; }
    }
}
