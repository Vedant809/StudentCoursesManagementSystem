using StudentCoursesSystem.DTOs;
using StudentCoursesSystem.Entities;

namespace StudentCoursesSystem.Interface
{
    public interface IEmployeeProject
    {
        IQueryable<Employee> GetEmployees();
        Task<int> AssignEmployeeProject(List<Entities.EmployeeProject> emp);
    }

    public interface IEmployeeProjectService
    {
        List<EmployeeDTO> GetAll();
        Task<int> AssignEmployeeProject(List<EmployeeProjectRequest> emp);
        EmployeeDTO GetEmployeeById(int empId);
    }
}
