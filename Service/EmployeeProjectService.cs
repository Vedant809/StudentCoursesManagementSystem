using StudentCoursesSystem.Entities;
using StudentCoursesSystem.Repository;
using StudentCoursesSystem.Interface;
using StudentCoursesSystem.DTOs;
using StudentCoursesSystem.CacheServer;

namespace StudentCoursesSystem.Service
{
    public class EmployeeProjectService:IEmployeeProjectService
    {
        private readonly IEmployeeProject _repo;
        private readonly CacheService _cacheService;
        private readonly TimeSpan _timeSpan = TimeSpan.FromDays(30);
        public EmployeeProjectService(IEmployeeProject repo,CacheService cacheService)
        {
            _repo = repo;
            _cacheService = cacheService;
        }

        public EmployeeDTO GetEmployeeById(int empId)
        {
            string key = $"AllEmployeeResultSet";
            var employeeList = _cacheService.GetOrSet(key, () => _repo.GetEmployees().ToList()
            , _timeSpan);
            var employee = employeeList.Where(x => x.Id == empId).FirstOrDefault();
            EmployeeDTO emp = new EmployeeDTO()
            {
                Id = employee.Id,
                EmployeeName = employee.EmployeeName,
                Department = employee.Department,
                Email = employee.Email,
                EmployeeProjects = employee?.EmployeeProjects?.Select(x => new DTOs.EmployeeProject
                {
                    Project = new ProjectDTO
                    {
                        ProjectId = x.Project.ProjectId,
                        Description = x.Project.Description,
                        Title = x.Project.Title,
                        StartDate = x.Project.StartDate,
                        EndDate = x.Project.EndDate
                    },
                    Role = x.Role,
                    HoursWorked = x.HoursWorked

                }).ToList()
            };
            return emp;
        }

        public List<EmployeeDTO> GetAll()
        {
            try
            {
                string key = $"AllEmployeeResultSet";
                //_cacheService.Remove(key);
                var list = _cacheService.GetOrSet(key, ()=>_repo.GetEmployees().ToList(), _timeSpan);
                //var list = _repo.GetEmployees().ToList();
                List<EmployeeDTO> res = new List<EmployeeDTO>();
                foreach(var item in list)
                {
                    EmployeeDTO emp = new EmployeeDTO
                    {
                        Id = item.Id,
                        EmployeeName = item.EmployeeName,
                        Email = item.Email,
                        Department = item.Department,
                        EmployeeProjects = item?.EmployeeProjects?.Select(x => new DTOs.EmployeeProject
                        {
                            Project = new ProjectDTO
                            {
                                ProjectId = x.ProjectId,
                                Title = x.Project?.Title,
                                Description = x.Project?.Description,
                                StartDate = x.Project?.StartDate,
                                EndDate = x.Project?.EndDate
                            },
                            Role = x.Role,
                            HoursWorked = x.HoursWorked
                        }).ToList()
                    };
                    res.Add(emp);
                }
                return res;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error fetching employees: {ex.Message}");
                return new List<EmployeeDTO>();
            }
        }

        public async Task<int> AssignEmployeeProject(List<EmployeeProjectRequest> emp)
        {
            List<Entities.EmployeeProject> bulkInsert = new List<Entities.EmployeeProject>();
            foreach(var item in emp)
            {
                Entities.EmployeeProject obj = new Entities.EmployeeProject
                {
                   EmployeeId = item.EmployeeId,
                   ProjectId = item.ProjectId,
                   Role = item.Role,
                   HoursWorked = item.HoursWorked
                };
                bulkInsert.Add(obj);
            }
            var result = await _repo.AssignEmployeeProject(bulkInsert);
            return result;
        }
    }
}
