using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using StudentCoursesSystem.Entities;
using StudentCoursesSystem.Interface;

namespace StudentCoursesSystem.Repository
{
    public class EmployeeProjectRepository:IEmployeeProject
    {
        private readonly APIDbContext _context;

        public EmployeeProjectRepository(APIDbContext context)
        {
            _context = context;
        }

        public IQueryable<Employee> GetEmployees()
        {
            var list = _context?.Employees?
                .Include(x => x.EmployeeProjects)
                .ThenInclude(x => x.Project)
                .AsNoTracking()
                .AsQueryable();
            return list;
        }

        public async Task<int> AssignEmployeeProject(List<EmployeeProject> emp)
        {
            _context.EmployeeProjects.AddRange(emp);
            await _context.SaveChangesAsync();
            return 1;
        }
    }
}
