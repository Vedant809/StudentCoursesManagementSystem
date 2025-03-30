using StudentCoursesSystem.Entities;

namespace StudentCoursesSystem.Interface
{
    public interface IStudentCoursesRepository
    {
        Task<int> Insert(StudentCourses entity);
        Student? getAll(int studentId);
        Course getStudentByCourse(int id);
        Task<int> RemoveStudent(StudentCourses entity);
        IQueryable<StudentCourses> getAllStudentCourses();
    }
}
