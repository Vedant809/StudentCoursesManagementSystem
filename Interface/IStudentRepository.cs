using StudentCoursesSystem.Entities;

namespace StudentCoursesSystem.Interface
{
    public interface IStudentRepository
    {
        Task<int> InsertStudent(Student student);
        IQueryable<Student> GetAll();
        Task<int> UpdateStudent(Student student);
        Task<int> DeleteStudent(Student student);
    }
}
