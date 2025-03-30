using Microsoft.EntityFrameworkCore;
using StudentCoursesSystem.Entities;
using StudentCoursesSystem.Interface;

namespace StudentCoursesSystem.Repository
{
    public class StudentCourseRepository:IStudentCoursesRepository
    {
        private readonly APIDbContext _context;
        public StudentCourseRepository(APIDbContext context)
        {
            _context = context;
        }
        public async Task<int> Insert(StudentCourses entity)
        {
            _context.StudentCourses.Add(entity);
            return await _context.SaveChangesAsync();
        }

        //Course related to a particular student
        public Student? getAll(int studentId)
        {
            if(_context == null)
            {
                return new Student();
            }
            return _context?.Students?.Where(x => x.Id == studentId)
                .Include(x => x.StudentCourses)
                .ThenInclude(x => x.Course)
                .AsNoTracking()
                .FirstOrDefault();

        }

        //Student related to a particular Course
        public Course getStudentByCourse(int id)
        {
            var result = _context?.Courses?.Where(x => x.Id == id)
                .Include(x => x.StudentCourses)
                .ThenInclude(x => x.Student)
                .AsNoTracking()
                .FirstOrDefault();
            return result;
        }

        //Remove a student from the course
        public async Task<int> RemoveStudent(StudentCourses entity)
        {
            _context.StudentCourses.Remove(entity);
            return await _context.SaveChangesAsync();
        }
        public IQueryable<StudentCourses> getAllStudentCourses()
        {
            return _context.StudentCourses.AsQueryable();
        }
    }
}
