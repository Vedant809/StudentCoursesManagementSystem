using StudentCoursesSystem.Entities;
using StudentCoursesSystem.Interface;

namespace StudentCoursesSystem.Repository
{
    public class CourseRepository:ICourseRepository
    {
        private readonly APIDbContext _context;
        public CourseRepository(APIDbContext context)
        {
            _context = context;
        }
        public async Task<int> InsertCourse(Course entity)
        {
            _context.Courses.Add(entity);
            return await _context.SaveChangesAsync();
        }
        public IQueryable<Course> GetAll()
        {
            return _context.Courses.AsQueryable();
        }
        public async Task<int> UpdateCourse(Course entity)
        {
            _context.Courses.Update(entity);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteStudent(Course entity)
        {
            _context.Courses.Remove(entity);
            return await _context.SaveChangesAsync();
        }

    }
}
