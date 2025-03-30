using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentCoursesSystem.DTOs;
using StudentCoursesSystem.Interface;

namespace StudentCoursesSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;
        public StudentController(IStudentService service)
        {
            _service = service;
        }

        [HttpPost("AddStudent")]
        public async Task<IActionResult> InsertStudent(StudentDTO request)
        {
            try
            {
                var result = await _service.AddStudent(request);
                return Ok(result);
            }
            catch
            {
                throw;
                throw;
            }
        }

        [HttpPost("UpdateStudent")]
        public async Task<IActionResult> Update(StudentDTO request)
        {
            try
            {
                var result = await _service.UpdateStudent(request);
                return Ok(result);
            }
            catch
            {
                throw;
                throw;
            }
        }

        [HttpPost("GetAllStudents")]
        public IActionResult GetAll(PaginationRequest request)
        {
            try
            {
                var result = _service.GetAll(request);
                return Ok(result);
            }
            catch
            {
                throw;
                throw;
            }
        }
    }
}
