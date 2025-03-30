namespace StudentCoursesSystem.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string? EmployeeName { get; set; }
        public string? Email { get; set; }
        public string? Department { get; set; }
        public virtual ICollection<EmployeeProject>? EmployeeProjects { get; set; }

    }
}
