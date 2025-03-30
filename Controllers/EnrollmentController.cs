using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentCoursesSystem.DTOs;
using StudentCoursesSystem.Entities;
using StudentCoursesSystem.Interface;

namespace StudentCoursesSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IStudentCoursesService _service;
        public EnrollmentController(IStudentCoursesService service)
        {
            _service = service;
        }

        [HttpPost("EnrollStudent")]
        public async Task<IActionResult> Add(StudentCoursesDTO request)
        {
            var result = await _service.Add(request);
            return Ok(result);
        }

        [HttpGet("GetCourseByStudent")]
        public StudentResponse get(int id)
        {
            var list = _service.get(id);
            return list;
        }

        [HttpGet("GetStudentByCourse")]
        public IActionResult GetStudentByCourse(int id)
        {
            try
            {
                var result = _service.getStudentByCourse(id);
                return Ok(result);
            }
            catch
            {
                throw;
                throw;
            }
        }

        [HttpPost("RemoveStudentFromClass")]
        public async Task<IActionResult> Remove(StudentCoursesDTO request)
        {
            try
            {
                var result = await _service.Remove(request);
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
