using StudentCoursesSystem.DTOs;
using StudentCoursesSystem.Entities;
using StudentCoursesSystem.Interface;

namespace StudentCoursesSystem.Service
{
    public class StudentCoursesService:IStudentCoursesService
    {
        private readonly IStudentCoursesRepository _repo;
        public StudentCoursesService(IStudentCoursesRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> Add(StudentCoursesDTO request)
        {
            StudentCourses entity = new StudentCourses()
            {
                StudentId = request.StudentId,
                CourseId = request.CourseId
            };
            var result = await _repo.Insert(entity);
            return result;
        }
        public StudentResponse get(int id)
        {
            var result = _repo.getAll(id);
            if(result == null)
            {
                return new StudentResponse
                {
                    StudentCourses = null
                };
            }
            StudentResponse res = new StudentResponse()
            {
                Id = result.Id,
                DOB = result.DOB,
                Address = result.Address,
                StudentName = result.StudentName,
                StudentCourses = result.StudentCourses?.Select(x=>new StudentCoursesResponse
                {
                    Courses = new CourseDTO
                    {
                        Id = x.Course.Id,
                        CourseName = x.Course.CourseName
                    }

                }).ToList()

            };
            return res;
        }

        public CourseResponse getStudentByCourse(int id)
        {
            var result = _repo.getStudentByCourse(id);
            CourseResponse res = new CourseResponse()
            {
                CourseId = result.Id,
                CourseName = result.CourseName,
                StudentCourses = result?.StudentCourses?.Select(x => new StudentCoursesResponse
                {
                    Students = new StudentDTO
                    {
                        Id = x.StudentId,
                        StudentName = x.Student.StudentName,
                        Address = x.Student.Address,
                        DOB = x.Student.DOB.Value
                    }

                }).ToList()

            };
            return res;
        }

        public async Task<int> Remove(StudentCoursesDTO request)
        {
            var existingEntity =  _repo.getAllStudentCourses()
                .Where(x => x.StudentId == request.StudentId && x.CourseId == request.CourseId).FirstOrDefault();
            if(existingEntity == null)
            {
                return 0;
            }
            
            var result = await _repo.RemoveStudent(existingEntity);
            return result;
        }
    }
}
