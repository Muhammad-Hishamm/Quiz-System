using Examination_System.Data;
using Examination_System.DTOs.Choices;
using Examination_System.DTOs.Courses;
using Examination_System.DTOs.Exams;
using Examination_System.DTOs.Feedbacks;
using Examination_System.DTOs.Instructors;
using Examination_System.DTOs.Questions;
using Examination_System.DTOs.Students;
using Examination_System.DTOs.Results;
using Examination_System.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

namespace Examination_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Context conte = new Context();
            DataInitializer.Initialize(conte);

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddAutoMapper(typeof(CourseProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(InstructorProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(ChoiceProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(StudentProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(ExamProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(FeedbackProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(QuestionProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(ResultProfile).Assembly);

            builder.Services.AddScoped<InstructorService>();
            builder.Services.AddScoped<CourseService>();
            builder.Services.AddScoped<StudentService>();
            builder.Services.AddScoped<ChoiceService>();
            builder.Services.AddScoped<ExamService>();
            builder.Services.AddScoped<FeedbackService>();
            builder.Services.AddScoped<QuestionService>();
            builder.Services.AddScoped<ResultService>();

            builder.Services.AddSwaggerGen();

            var key = Encoding.ASCII.GetBytes(Constants.SecretKey);
            builder.Services.AddAuthentication(opt=>             
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt => {
                    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key),
                        ValidIssuer= "",
                        ValidAudience= "",
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    
                    };
                });
            builder.Services.AddAuthorization();

            var app = builder.Build();
            app.UseAuthentication();
            //app.UseAuthorization();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
