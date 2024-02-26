using Microsoft.EntityFrameworkCore;
using RestApiTesting.Models;

namespace RestApiTesting.Services
{
    public class JobHistoryRepository : IJobHistoryService
    {
        private readonly RestApiTestingContext _context;
        public JobHistoryRepository(RestApiTestingContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JobHistoryWithDetails>> GetJobHistoryWithDetailsEmployeeAgeBetween30And40AndPositionSalaryGreaterThan1000()
        {
            return await _context.JobHistories
                .Include(x => x.Employee)
                .Include(x => x.Position)
                .Include(x => x.Position.JobHistories)
                .Include(x => x.Employee.Department)
                .Where(x => x.Employee.Age >= 30 && x.Employee.Age <= 40 && x.Position.Salary > 1000)
                .Select(x => new JobHistoryWithDetails
                {
                    Title = x.Title,
                    Description = x.Description,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Employee = x.Employee,
                    Position = x.Position,
                    Department = x.Employee.Department
                })
                .ToListAsync();
        }
    }

    public interface IJobHistoryService
    {
        // Get JobHistoryWithDetails has employee with age between 30 and 40 and position with salary greater than 1000
        Task<IEnumerable<JobHistoryWithDetails>> GetJobHistoryWithDetailsEmployeeAgeBetween30And40AndPositionSalaryGreaterThan1000();
    }

    // Generate a new class has property of Position, Employee and Department 
    public class JobHistoryWithDetails
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public Employee Employee { get; set; } = null!;
        public Position Position { get; set; } = null!;
        public Department Department { get; set; } = null!;
    }
}
