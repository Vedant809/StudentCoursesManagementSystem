using StudentCoursesSystem.DTOs;
using StudentCoursesSystem.Entities;
using StudentCoursesSystem.Interface;

namespace StudentCoursesSystem.Service
{
    public class CourseService:ICourseService
    {
        private readonly ICourseRepository _repo;
        public CourseService(ICourseRepository repo)
        {
            _repo = repo;
        }
        public async Task<int> AddCourse(CourseDTO request)
        {
            Course entity = new Course()
            {
                CourseName = request.CourseName
            };
            return await _repo.InsertCourse(entity);
        }
        public async Task<int> UpdateCourse(CourseDTO request)
        {
            var existingEntity = _repo.GetAll().Where(x => x.Id == request.Id).FirstOrDefault();
            existingEntity.CourseName = request.CourseName;
            return await _repo.UpdateCourse(existingEntity);
        }
        public List<CourseDTO> GetAll()
        {
            List<CourseDTO> finalList = new List<CourseDTO>();
            var courseList = _repo.GetAll().ToList();
            foreach(var item in courseList)
            {
                CourseDTO course = new CourseDTO()
                {
                    Id = item.Id,
                    CourseName = item.CourseName
                };
                finalList.Add(course);
            }
            return finalList;
        }

    }
}
