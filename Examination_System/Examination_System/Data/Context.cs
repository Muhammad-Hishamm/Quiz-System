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

            // StudentCourse M2M
            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId);

            // StudentExam M2M
            modelBuilder.Entity<StudentExam>()
                .HasKey(se => new { se.StudentId, se.ExamId });

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


            // QuizQuestion M2M
            modelBuilder.Entity<ExamQuestion>()
                .HasKey(eq => new { eq.ExamId, eq.QuestionId });

            modelBuilder.Entity<ExamQuestion>()
                .HasOne(eq => eq.Exam)
                .WithMany(e => e.ExamQuestions)
                .HasForeignKey(eq => eq.ExamId)
                .OnDelete(DeleteBehavior.Restrict); //  prevents cascade path

            modelBuilder.Entity<ExamQuestion>()
                .HasOne(eq => eq.Question)
                .WithMany(q => q.ExamQuestions)
                .HasForeignKey(eq => eq.QuestionId)
                .OnDelete(DeleteBehavior.Restrict); //  prevents cascade path


            // Question → Choice (1-to-Many)
            modelBuilder.Entity<Question>()
                .HasMany(q => q.Choices)
                .WithOne(c => c.Question)
                .HasForeignKey(c => c.QuestionId);

            // Course → Quiz (1-to-Many)
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Exams)
                .WithOne(q => q.Course)
                .HasForeignKey(q => q.CourseId);

            // Instructor → Course (1-to-Many)
            modelBuilder.Entity<Instructor>()
                .HasMany(i => i.Courses)
                .WithOne(c => c.Instructor)
                .HasForeignKey(c => c.InstructorId);

            // Instructor → Question (1-to-Many)
            modelBuilder.Entity<Instructor>()
                .HasMany(i => i.Questions)
                .WithOne(q => q.Instructor)
                .HasForeignKey(q => q.InstructorId);
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Result> Results { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<StudentExam> StudentExams { get; set; }
        public DbSet<ExamQuestion> ExamQuestions { get; set; }





    }
}
