namespace StudentCoursesSystem.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public virtual ICollection<EmployeeProject>? EmployeeProjects { get; set; }
    }
}
