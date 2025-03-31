using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentCoursesSystem.DTOs;
using StudentCoursesSystem.Entities;
using StudentCoursesSystem.Interface;

namespace StudentCoursesSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class EmployeeProjectController : ControllerBase
    {
        private readonly IEmployeeProjectService _service;
        public EmployeeProjectController(IEmployeeProjectService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetEmployee")]
        public IActionResult getEmp()
        {
            try
            {
                var list = _service.GetAll();
                return Ok(list);
            }
            catch
            {
                throw;
                throw;
            }
            
        }

        [HttpPost("AssignEmployeeProject")]
        public async Task<IActionResult> Add(List<EmployeeProjectRequest> emp)
        {
            var result = await _service.AssignEmployeeProject(emp);
            return Ok(result);
        }

        [Authorize(Roles = "Supplier")]
        [HttpGet("GetEmployeeById")]
        public IActionResult get(int empId)
        {
            try
            {
                var result = _service.GetEmployeeById(empId);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
