using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StudentCoursesSystem.Entities;
using StudentCoursesSystem.Interface;
using StudentCoursesSystem.Middleware;
using StudentCoursesSystem.Repository;
using StudentCoursesSystem.Service;
using StudentCoursesSystem.CacheServer;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var key = builder.Configuration["JwtSettings:Secret"];

// Add services to the container.
builder.Services.AddDbContext<APIDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("dbconnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<APIDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });

    // Add JWT Authentication to Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter 'Bearer YOUR_TOKEN_HERE'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("VGhpcy1pcy1hLXNlY3VyZS1zZWNyZXQta2V5LWZvci1KV1QtU2lnbmluZw==\r\n")),
        ValidateIssuer = false,
        ValidateAudience = false,
        RequireExpirationTime = true,
        ValidateLifetime = true
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

//Register in memory cache
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<CacheService>();

builder.Services.AddControllers();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();

builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICourseService, CourseService>();

builder.Services.AddScoped<IStudentCoursesRepository, StudentCourseRepository>();
builder.Services.AddScoped<IStudentCoursesService, StudentCoursesService>();

builder.Services.AddScoped<IEmployeeProject, EmployeeProjectRepository>();
builder.Services.AddScoped<IEmployeeProjectService, EmployeeProjectService>();

builder.Services.AddSingleton<ILoggerFactory, LoggerFactory>();

builder.Services.AddLogging();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.UseMiddleware<LoggingMiddleware>();

app.Run();
