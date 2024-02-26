using Microsoft.EntityFrameworkCore;
using Moq;
using RestApi.Test.xUnit.TestProvider;
using RestApiTesting.Models;
using RestApiTesting.Services;
using System.Linq;

namespace RestApi.Test.xUnit.Repository
{
    public class EmployeeRepositoryTest
    {
        private Mock<RestApiTestingContext> _context = new Mock<RestApiTestingContext>();
        private EmployeeRepository _employeeRepository;
        public EmployeeRepositoryTest()
        {
            _employeeRepository = new EmployeeRepository(_context.Object);
        }

        // Write test for CreateEmployee method
        [Fact]
        public void CreateEmployee_ReturnsData()
        {
            // Arrange
            var employee = new Employee
            {
                Id = new Guid(),
                Name = "John",
                AccountNumber = "123456"
            };
            _context.Setup(x => x.Employees.Add(employee));
            _context.Setup(x => x.SaveChanges());

            // Act
            _employeeRepository.CreateEmployee(employee);

            // Assert
            _context.Verify(x => x.Employees.Add(employee), Times.Once);
            _context.Verify(x => x.SaveChanges(), Times.Once);
        }

        // Write test for GetAllEmployees method return list of employees
        [Fact]
        public async Task GetAllEmployees_ReturnsData()
        {
            // Arrange
            var employees = new TestAsyncEnumerable<Employee>(new List<Employee>
            {
                new Employee
                {
                    Id = new Guid(),
                    Name = "John",
                    AccountNumber = "123456"
                },
                new Employee
                {
                    Id = new Guid(),
                    Name = "Jane",
                    AccountNumber = "654321"
                }
            }).AsQueryable();
            
            var mockSet = new Mock<DbSet<Employee>>();

            mockSet.As<IAsyncEnumerable<Employee>>().Setup(m => m.GetAsyncEnumerator(new CancellationToken())).Returns(new TestAsyncEnumerator<Employee>(employees.GetEnumerator()));
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<Employee>(employees.Provider));
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(employees.Expression);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(employees.ElementType);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(employees.GetEnumerator());
            _context.Setup(x => x.Employees).Returns(mockSet.Object);
            // Act
            var result = await _employeeRepository.GetAllEmployees();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Employee>>(result);
            Assert.Equal(2, result.Count());
        }

        // Write test for GetAverageAge method return average age of employees
        [Fact]
        public async Task GetAverageAge_ReturnsData()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee
                {
                    Id = new Guid(),
                    Name = "John",
                    Age = 25,
                    AccountNumber = "123456"
                },
                new Employee
                {
                    Id = new Guid(),
                    Name = "Jane",
                    Age = 30,
                    AccountNumber = "654321"
                }
            }.AsQueryable();
            var mockSet = new Mock<DbSet<Employee>>();
            mockSet.As<IAsyncEnumerable<Employee>>().Setup(m => m.GetAsyncEnumerator(default))
                .Returns(new TestAsyncEnumerator<Employee>(employees.GetEnumerator()));
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<Employee>(employees.Provider));
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(employees.Expression);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(employees.ElementType);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(employees.GetEnumerator());

            _context.Setup(x => x.Employees).Returns(mockSet.Object);
            // Act
            var result = await _employeeRepository.GetAverageAge();

            // Assert
            Assert.All(employees, x => Assert.IsType<Employee>(x));
            Assert.IsType<double>(result);
            Assert.Equal(27.5, result);
        }

        // Write test for GetMaxAge method return maximum age of employees
        [Fact]
        public async Task GetMaxAge_ReturnsData()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee
                {
                    Id = new Guid(),
                    Name = "John",
                    Age = 25,
                    AccountNumber = "123456"
                },
                new Employee
                {
                    Id = new Guid(),
                    Name = "Jane",
                    Age = 30,
                    AccountNumber = "654321"
                }
            }.AsQueryable();
            var mockSet = new Mock<DbSet<Employee>>();
            mockSet.As<IAsyncEnumerable<Employee>>().Setup(m => m.GetAsyncEnumerator(default))
                .Returns(new TestAsyncEnumerator<Employee>(employees.GetEnumerator()));
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<Employee>(employees.Provider));
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(employees.Expression);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(employees.ElementType);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(employees.GetEnumerator());

            _context.Setup(x => x.Employees).Returns(mockSet.Object);
            // Act
            var result = await _employeeRepository.GetMaxAge();

            // Assert
            Assert.All(employees, x => Assert.IsType<Employee>(x));
            Assert.IsType<int>(result);
            Assert.Equal(30, result);
        }

        // Write test for GetMinAge method return minimum age of employees
        [Fact]
        public async Task GetMinAge_ReturnsData()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee
                {
                    Id = new Guid(),
                    Name = "John",
                    Age = 25,
                    AccountNumber = "123456"
                },
                new Employee
                {
                    Id = new Guid(),
                    Name = "Jane",
                    Age = 30,
                    AccountNumber = "654321"
                }
            }.AsQueryable();
            var mockSet = new Mock<DbSet<Employee>>();
            mockSet.As<IAsyncEnumerable<Employee>>().Setup(m => m.GetAsyncEnumerator(default))
                .Returns(new TestAsyncEnumerator<Employee>(employees.GetEnumerator()));
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<Employee>(employees.Provider));
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(employees.Expression);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(employees.ElementType);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(employees.GetEnumerator());

            _context.Setup(x => x.Employees).Returns(mockSet.Object);
            // Act
            var result = await _employeeRepository.GetMinAge();

            // Assert
            Assert.All(employees, x => Assert.IsType<Employee>(x));
            Assert.IsType<int>(result);
            Assert.Equal(25, result);
        }

        // Write test for GetEmployeesByNameAndAge method return list of employees
        [Fact]
        public async Task GetEmployeesByNameAndAge_ReturnsData()
        {
            // Arrange
            var employees = new TestAsyncEnumerable<Employee>(new List<Employee>
            {
                new Employee
                {
                    Id = new Guid(),
                    Name = "John",
                    Age = 25,
                    AccountNumber = "123456"
                },
                new Employee
                {
                    Id = new Guid(),
                    Name = "Jane",
                    Age = 31,
                    AccountNumber = "654321"
                }
            }).AsQueryable();
            var mockSet = new Mock<DbSet<Employee>>();
            mockSet.As<IAsyncEnumerable<Employee>>().Setup(m => m.GetAsyncEnumerator(default))
                .Returns(new TestAsyncEnumerator<Employee>(employees.GetEnumerator()));
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<Employee>(employees.Provider));
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(employees.Expression);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(employees.ElementType);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(employees.GetEnumerator());

            _context.Setup(x => x.Employees).Returns(mockSet.Object);
            // Act
            var result = await _employeeRepository.GetEmployeesByNameAndAge();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Employee>>(result);
            Assert.Equal(1, result.Count());
        }

        // Write test for GetEmployeesByDepartment method return list of employees
        [Fact]
        public async Task GetEmployeesByDepartment_ReturnsData()
        {
            var departmentId = new Guid();
            // Arrange
            var employees = new TestAsyncEnumerable<Employee>(new List<Employee>
            {
                new Employee
                {
                    Id = new Guid(),
                    Name = "John",
                    Age = 25,
                    AccountNumber = "123456",
                    DepartmentId = departmentId
                },
                new Employee
                {
                    Id = new Guid(),
                    Name = "Jane",
                    Age = 31,
                    AccountNumber = "654321",
                    DepartmentId = departmentId
                }
            }).AsQueryable();
            var mockSet = new Mock<DbSet<Employee>>();
            mockSet.As<IAsyncEnumerable<Employee>>().Setup(m => m.GetAsyncEnumerator(default))
                .Returns(new TestAsyncEnumerator<Employee>(employees.GetEnumerator()));
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<Employee>(employees.Provider));
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(employees.Expression);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(employees.ElementType);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(employees.GetEnumerator());

            _context.Setup(x => x.Employees).Returns(mockSet.Object);
            // Act
            var result = await _employeeRepository.GetEmployeesByDepartment(new Guid());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Employee>>(result);
            Assert.Equal(2, result.Count());
            // Assert that all employees are from the same department
            Assert.All(result, x => Assert.Equal(departmentId, x.DepartmentId));
        }

        // Write test for GetEmployeesGroupedByDepartment method return list of employees grouped by department
        [Fact]
        public async Task GetEmployeesGroupedByDepartment_ReturnsData()
        {
            var departmentOne = new Guid();
            var departmentTwo = new Guid();
            // Arrange
            var employees = new List<Employee>
            {
                new Employee
                {
                    Id = new Guid(),
                    Name = "John",
                    Age = 25,
                    AccountNumber = "123456",
                    DepartmentId = departmentOne
                },
                new Employee
                {
                    Id = new Guid(),
                    Name = "Jane",
                    Age = 31,
                    AccountNumber = "654321",
                    DepartmentId = departmentTwo
                }
            }.AsQueryable();
            var mockSet = new Mock<DbSet<Employee>>();
            mockSet.As<IAsyncEnumerable<Employee>>().Setup(m => m.GetAsyncEnumerator(default))
                .Returns(new TestAsyncEnumerator<Employee>(employees.GetEnumerator()));
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<Employee>(employees.Provider));
            mockSet.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(employees.Expression);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(employees.ElementType);
            mockSet.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(employees.GetEnumerator());

            _context.Setup(x => x.Employees).Returns(mockSet.Object);
            // Act
            var result = await _employeeRepository.GetEmployeesGroupedByDepartment();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<IGrouping<Guid, Employee>>>(result);
            // Assert that all employees has departmentone or departmenttwo as departmentId
            Assert.All(result, x => Assert.Contains(departmentOne, x.Select(y => y.DepartmentId)));
            Assert.All(result, x => Assert.Contains(departmentTwo, x.Select(y => y.DepartmentId)));
        }
    }

}
