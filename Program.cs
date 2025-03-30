using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StudentCoursesSystem.Entities;
using StudentCoursesSystem.Interface;
using StudentCoursesSystem.Middleware;
using StudentCoursesSystem.Repository;
using StudentCoursesSystem.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<APIDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("dbconnection")));


builder.Services.AddControllers();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();

builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseService, CourseService>();

builder.Services.AddScoped<IStudentCoursesRepository, StudentCourseRepository>();
builder.Services.AddScoped<IStudentCoursesService, StudentCoursesService>();

builder.Services.AddSingleton<ILoggerFactory, LoggerFactory>();
//builder.Services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

builder.Services.AddLogging();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<LoggingMiddleware>();

app.Run();
