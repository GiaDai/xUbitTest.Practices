namespace RestApiTesting.Models
{
    public class Department
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        // Department has a collection of employees
        public ICollection<Employee> Employees { get; set; }
        public decimal AverageSalary { get; internal set; }
    }
}
