using Moq;
using RestApiTesting.Models;
using RestApiTesting.Services;

namespace RestApi.Test.xUnit.Repository
{
    public class JobHistoryRepositoryTest
    {
        private Mock<RestApiTestingContext> _context = new Mock<RestApiTestingContext>();
        private JobHistoryRepository _jobHistoryRepository;

        public JobHistoryRepositoryTest()
        {
            _jobHistoryRepository = new JobHistoryRepository(_context.Object);
        }

        // Test GetJobHistoryWithDetailsEmployeeAgeBetween30And40AndPositionSalaryGreaterThan1000
        [Fact]
        public async Task GetJobHistoryWithDetailsEmployeeAgeBetween30And40AndPositionSalaryGreaterThan1000()
        {
            var employeeId = new Guid();
            var positionOne = new Guid();
            var positionTwo = new Guid();
            var positionThree = new Guid();
            var departmentId = new Guid();
            // Arrange
            var jobHistories = new List<JobHistory>
            {
                new JobHistory
                {
                    Title = "Software Engineer",
                    Description = "Developing software",
                    StartDate = "2020-01-01",
                    EndDate = "2021-01-01",
                    EmployeeId = employeeId,
                    PositionId = positionOne
                },
                new JobHistory
                {
                    Title = "Software Engineer",
                    Description = "Developing software",
                    StartDate = "2021-01-01",
                    EndDate = "2022-01-01",
                    EmployeeId = employeeId,
                    PositionId = positionTwo
                },
                new JobHistory
                {
                    Title = "Software Engineer",
                    Description = "Developing software",
                    StartDate = "2022-01-01",
                    EndDate = "2023-01-01",
                    EmployeeId = employeeId,
                    PositionId = positionThree
                }
            };
            var employees = new List<Employee>
            {
                new Employee
                {
                    Id = employeeId,
                    Name = "John",
                    Age = 35,
                    Level = 3,
                    Salary = 2000,
                    DepartmentId = employeeId
                }
            };
            var positions = new List<Position>
            {
                new Position
                {
                    Id = positionOne,
                    Name = "Software Engineer",
                    Description = "Developing software",
                    Salary = 2000
                }
            };
            var departments = new List<Department>
            {
                new Department
                {
                    Id = departmentId,
                    Name = "IT"
                }
            };

            _context.Setup(x => x.JobHistories).ReturnsDbSet(jobHistories);
            _context.Setup(x => x.Employees).ReturnsDbSet(employees);
            _context.Setup(x => x.Positions).ReturnsDbSet(positions);
            _context.Setup(x => x.Departments).ReturnsDbSet(departments);

            // Act
            var result = await _jobHistoryRepository.GetJobHistoryWithDetailsEmployeeAgeBetween30And40AndPositionSalaryGreaterThan1000();

        }
    }
}
