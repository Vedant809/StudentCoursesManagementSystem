using System.Text.Json.Serialization;

namespace StudentCoursesSystem.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string? StudentName { get; set; }
        public DateTime? DOB { get; set; }
        public string? Address { get; set; }

        [JsonIgnore]
        public virtual ICollection<StudentCourses>? StudentCourses { get; set; }
    }
}
