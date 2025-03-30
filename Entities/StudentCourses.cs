namespace StudentCoursesSystem.Entities
{
    public class StudentCourses
    {
        public int StudentId { get; set; }
        public virtual Student? Student { get; set; }
        public int CourseId { get; set; }
        public virtual Course? Course { get; set; }
    }
}
