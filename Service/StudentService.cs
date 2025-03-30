using StudentCoursesSystem.DTOs;
using StudentCoursesSystem.Entities;
using StudentCoursesSystem.Interface;

namespace StudentCoursesSystem.Service
{
    public class StudentService:IStudentService
    {
        private readonly IStudentRepository _repo;
        public StudentService(IStudentRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> AddStudent(StudentDTO request)
        {
            Student entity = new Student();
            entity.StudentName = request.StudentName;
            entity.Address = request.Address;
            entity.DOB = request.DOB;

            var result = await _repo.InsertStudent(entity);
            return result;
        }
        public async Task<int> UpdateStudent(StudentDTO request)
        {
            var existingEntity = _repo.GetAll().Where(x => x.Id == request.Id).FirstOrDefault();

            if(existingEntity == null)
            {
                return 0;
            }

            existingEntity.StudentName = request.StudentName;
            existingEntity.DOB = request.DOB;
            existingEntity.Address = request.Address;

            var result = await _repo.UpdateStudent(existingEntity);
            return result;
            
        }
        public PaginationResponse GetAll(PaginationRequest request)
        {
            PaginationResponse response = new();
            var list = _repo.GetAll().ToList();
            var paginatedList = list
                .Skip(request.PageSize * (request.Index - 1))
                .Take(request.PageSize);
            response.TotalCount = paginatedList.Count();
            List<StudentDTO> finalList = new List<StudentDTO>();
            foreach(var item in paginatedList)
            {
                StudentDTO student = new StudentDTO()
                {
                    Id = item.Id,
                    StudentName = item.StudentName,
                    DOB = item.DOB.GetValueOrDefault(),
                    Address = item.Address
                };
                finalList.Add(student);
            }

            response.Index = request.Index;
            response.PageSize = request.PageSize;
            response.StudentInformation = finalList;

            return response;
        }

    }
}
