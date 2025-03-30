using StudentCoursesSystem.Entities;
using System.Diagnostics.Contracts;

namespace StudentCoursesSystem.DTOs
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string? StudentName { get; set; }
        public string? Address { get; set; }
        public DateTime DOB { get; set; }
    }

    public class PaginationResponse
    {
        public int Index { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public List<StudentDTO> StudentInformation { get; set; } = new List<StudentDTO>();
    }

    public class PaginationRequest
    {
        public int Index { get; set; }
        public int PageSize { get; set; }
        public string? Search { get; set; }
    }
    public class CourseDTO
    {
        public int Id { get; set; }
        public string? CourseName { get; set; }
    }
    public class StudentCoursesDTO
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }

    public class StudentCoursesResponse
    {
        public StudentDTO Students { get; set; }
        public CourseDTO Courses { get; set; }
    }

    public class CourseResponse
    {
        public int CourseId { get; set; }
        public string? CourseName { get; set; }
        public virtual List<StudentCoursesResponse>? StudentCourses { get; set; }
    }

    public class StudentResponse
    {
        public int Id { get; set; }
        public string? StudentName { get; set; }
        public DateTime? DOB { get; set; }
        public string? Address { get; set; }
        public virtual List<StudentCoursesResponse>? StudentCourses { get; set; }

    }

    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string? EmployeeName { get; set; }
        public string? Email { get; set; }
        public string? Department { get; set; }
        public List<EmployeeProject>? EmployeeProjects { get; set; }
        
    }
    public class ProjectDTO
    {
        public int ProjectId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
    public class EmployeeProject
    {
        public EmployeeDTO Employee { get; set; }
        public ProjectDTO Project { get; set; }
        public string? Role { get; set; }
        public int HoursWorked { get; set; }
    }

    public class EmployeeProjectRequest
    {
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }
        public string? Role { get; set; }
        public int HoursWorked { get; set; }
    }
    //public class EmployeeResponse
    //{

    //}
}
