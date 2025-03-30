using StudentCoursesSystem.DTOs;

namespace StudentCoursesSystem.Interface
{
    public interface IStudentService
    {
        Task<int> AddStudent(StudentDTO request);
        Task<int> UpdateStudent(StudentDTO request);
        PaginationResponse GetAll(PaginationRequest request);
    }
}
