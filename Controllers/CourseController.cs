using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentCoursesSystem.DTOs;
using StudentCoursesSystem.Interface;

namespace StudentCoursesSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _service;
        public CourseController(ICourseService service)
        {
            _service = service;
        }

        [HttpPost("AddCourse")]
        public async Task<IActionResult> AddCourse(CourseDTO request)
        {
            var result = await _service.AddCourse(request);
            return Ok(result);
        }

        [HttpPost("ÜpdateCourse")]
        public async Task<IActionResult> UpdateCourse(CourseDTO request)
        {
            var result = await _service.UpdateCourse(request);
            return Ok(result);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var list = _service.GetAll();
            return Ok(list);
        }

        //[HttpGet("GetStudentByCourse")]
        //public IActionResult GetStudentByCourse(int id)
        //{
        //    try
        //    {
        //        var result = _service.get(id);
        //        return Ok(result);
        //    }
        //    catch
        //    {
        //        throw;
        //        throw;
        //    }
        //}
    }
}
