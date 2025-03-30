using StudentCoursesSystem.DTOs;

namespace StudentCoursesSystem.Interface
{
    public interface ICourseService
    {
        Task<int> AddCourse(CourseDTO request);
        Task<int> UpdateCourse(CourseDTO request);
        List<CourseDTO> GetAll();
    }
}
