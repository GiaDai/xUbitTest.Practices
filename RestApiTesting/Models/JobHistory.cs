namespace RestApiTesting.Models
{
    public class JobHistory
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
        // Has a position
        public Guid PositionId { get; set; }
        public Position Position { get; set; } = null!;
    }
}
