using StudentCoursesSystem.Interface;
using StudentCoursesSystem.Entities;

namespace StudentCoursesSystem.Repository
{
    public class StudentRepository:IStudentRepository
    {
        private readonly APIDbContext _context;
        public StudentRepository(APIDbContext context)
        {
            _context = context;
        }
        public async Task<int> InsertStudent(Student student)
        {
            _context.Students.Add(student);
            return await _context.SaveChangesAsync();
        }

        public IQueryable<Student> GetAll()
        {
            var students = _context.Students.AsQueryable();
            return students;
        }
        public async Task<int> UpdateStudent(Student student)
        {
            _context.Students.Update(student);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteStudent(Student student)
        {
            _context.Students.Remove(student);
            return await _context.SaveChangesAsync();
        }

    }
}
