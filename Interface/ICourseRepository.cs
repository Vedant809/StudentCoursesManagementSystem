using StudentCoursesSystem.Entities;

namespace StudentCoursesSystem.Interface
{
    public interface ICourseRepository
    {
        Task<int> InsertCourse(Course entity);
        IQueryable<Course> GetAll();
        Task<int> UpdateCourse(Course entity);
        Task<int> DeleteStudent(Course entity);

    }
}
