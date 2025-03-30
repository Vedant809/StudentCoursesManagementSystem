using System.Text.Json.Serialization;

namespace StudentCoursesSystem.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string? CourseName { get; set; }

        [JsonIgnore]
        public virtual ICollection<StudentCourses>? StudentCourses { get; set; }
    }
}
