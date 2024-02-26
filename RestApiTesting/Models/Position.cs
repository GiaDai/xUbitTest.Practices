namespace RestApiTesting.Models
{
    public class Position
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public decimal Salary { get; set; }
        public string? Description { get; set;}
        // Position has many job histories
        public ICollection<JobHistory> JobHistories { get; set; }
    }
}
