using StudentCoursesSystem.DTOs;
using StudentCoursesSystem.Entities;

namespace StudentCoursesSystem.Interface
{
    public interface IStudentCoursesService
    {
        Task<int> Add(StudentCoursesDTO request);
        StudentResponse get(int id);
        CourseResponse getStudentByCourse(int id);
        Task<int> Remove(StudentCoursesDTO request);
    }
}
