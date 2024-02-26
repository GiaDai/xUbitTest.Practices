using Microsoft.EntityFrameworkCore;
using Moq;
using RestApi.Test.xUnit.TestProvider;
using RestApiTesting.Models;
using RestApiTesting.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApi.Test.xUnit.Repository
{
    public class DepartmentRepositoryTest
    {
        private Mock<RestApiTestingContext> _context = new Mock<RestApiTestingContext>();
        private DepartmentRepository _departmentRepository;
        public DepartmentRepositoryTest()
        {
            _departmentRepository = new DepartmentRepository(_context.Object);
        }

        // Write test for GetDepartmentsWithEmployeesBetween30And40
        [Fact]
        public async Task GetDepartmentsWithEmployeesBetween30And40_ShouldReturnDepartments()
        {
            // Arrange
            var departments = new List<Department>
            {
                new Department
                {
                    Id = new Guid(),
                    Name = "HR",
                    Employees = new List<Employee>
                    {
                        new Employee
                        {
                            Id = new Guid(),
                            Name = "John",
                            Age = 30
                        },
                        new Employee
                        {
                            Id = new Guid(),
                            Name = "Jane",
                            Age = 40
                        }
                    }
                },
                new Department
                {
                    Id = new Guid(),
                    Name = "IT",
                    Employees = new List<Employee>
                    {
                        new Employee
                        {
                            Id = new Guid(),
                            Name = "Tom",
                            Age = 25
                        },
                        new Employee
                        {
                            Id = new Guid(),
                            Name = "Jerry",
                            Age = 22
                        }
                    }
                }
            }.AsQueryable();
            var mockSet = new Mock<DbSet<Department>>();
            
            mockSet.As<IAsyncEnumerable<Department>>().Setup(m => m.GetAsyncEnumerator(default))
                .Returns(new TestAsyncEnumerator<Department>(departments.GetEnumerator()));
            mockSet.As<IQueryable<Department>>().Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<Department>(departments.Provider));
            mockSet.As<IQueryable<Department>>().Setup(m => m.Expression).Returns(departments.Expression);
            mockSet.As<IQueryable<Department>>().Setup(m => m.ElementType).Returns(departments.ElementType);
            mockSet.As<IQueryable<Department>>().Setup(m => m.GetEnumerator()).Returns(departments.GetEnumerator());

            // Act
            _context.Setup(x => x.Departments).Returns(mockSet.Object);
            var result = await _departmentRepository.GetDepartmentsWithEmployeesBetween30And40();
            // Assert
            Assert.Single(result);
            Assert.Equal("HR", result.First().Name);
            Assert.Equal(2, result.First().Employees.Count);
        }

        // Generate dummy department data with 10 records with 5 employees each.
        // Age's employee is between 18 and 6
        // Account's number is format like "12-456789456-789" with random number

        // Write test for GetDepartmentsWithEmployeesLevelLessThan3
        [Fact]
        public async Task GetDepartmentsWithEmployeesLevelLessThan3_ShouldReturnDepartments()
        {
            // Arrange
            var departments = new List<Department>
            {
                new Department
                {
                    Id = new Guid(),
                    Name = "HR",
                    Employees = new List<Employee>
                    {
                        new Employee
                        {
                            Id = new Guid(),
                            Name = "John",
                            Level = 1
                        },
                        new Employee
                        {
                            Id = new Guid(),
                            Name = "Jane",
                            Level = 2
                        }
                    }
                },
                new Department
                {
                    Id = new Guid(),
                    Name = "IT",
                    Employees = new List<Employee>
                    {
                        new Employee
                        {
                            Id = new Guid(),
                            Name = "Tom",
                            Level = 3
                        },
                        new Employee
                        {
                            Id = new Guid(),
                            Name = "Jerry",
                            Level = 4
                        }
                    }
                }
            }.AsQueryable();
            var mockSet = new Mock<DbSet<Department>>();
            
            mockSet.As<IAsyncEnumerable<Department>>().Setup(m => m.GetAsyncEnumerator(default))
                .Returns(new TestAsyncEnumerator<Department>(departments.GetEnumerator()));
            mockSet.As<IQueryable<Department>>().Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<Department>(departments.Provider));
            mockSet.As<IQueryable<Department>>().Setup(m => m.Expression).Returns(departments.Expression);
            mockSet.As<IQueryable<Department>>().Setup(m => m.ElementType).Returns(departments.ElementType);
            mockSet.As<IQueryable<Department>>().Setup(m => m.GetEnumerator()).Returns(departments.GetEnumerator());

            // Act
            _context.Setup(x => x.Departments).Returns(mockSet.Object);
            var result = await _departmentRepository.GetDepartmentsWithEmployeesLevelLessThan3();
            // Assert
            Assert.Single(result);
            Assert.Equal("HR", result.First().Name);
            Assert.Equal(2, result.First().Employees.Count);
        }

        // Write test for GetDepartmentsWithEmployeesLevelGreaterThan3
        [Fact]
        public async Task GetDepartmentsWithEmployeesLevelGreaterThan3_ShouldReturnDepartments()
        {
            // Arrange
            var departments = new List<Department>
            {
                new Department
                {
                    Id = new Guid(),
                    Name = "HR",
                    Employees = new List<Employee>
                    {
                        new Employee
                        {
                            Id = new Guid(),
                            Name = "John",
                            Level = 4
                        },
                        new Employee
                        {
                            Id = new Guid(),
                            Name = "Jane",
                            Level = 5
                        }
                    }
                },
                new Department
                {
                    Id = new Guid(),
                    Name = "IT",
                    Employees = new List<Employee>
                    {
                        new Employee
                        {
                            Id = new Guid(),
                            Name = "Tom",
                            Level = 1
                        },
                        new Employee
                        {
                            Id = new Guid(),
                            Name = "Jerry",
                            Level = 2
                        }
                    }
                }
            }.AsQueryable();
            var mockSet = new Mock<DbSet<Department>>();
            
            mockSet.As<IAsyncEnumerable<Department>>().Setup(m => m.GetAsyncEnumerator(default))
                .Returns(new TestAsyncEnumerator<Department>(departments.GetEnumerator()));
            mockSet.As<IQueryable<Department>>().Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<Department>(departments.Provider));
            mockSet.As<IQueryable<Department>>().Setup(m => m.Expression).Returns(departments.Expression);
            mockSet.As<IQueryable<Department>>().Setup(m => m.ElementType).Returns(departments.ElementType);
            mockSet.As<IQueryable<Department>>().Setup(m => m.GetEnumerator()).Returns(departments.GetEnumerator());

            // Act
            _context.Setup(x => x.Departments).Returns(mockSet.Object);
            var result = await _departmentRepository.GetDepartmentsWithEmployeesLevelGreaterThan3();
            // Assert
            Assert.Single(result);
            Assert.Equal("HR", result.First().Name);
            Assert.Equal(2, result.First().Employees.Count);
        }

        // Write test for GetDepartmentsWithAverageSalary
        [Fact]
        public async Task GetDepartmentsWithAverageSalary_ShouldReturnDepartments()
        {
            // Arrange
            var departments = new List<Department>
            {
                new Department
                {
                    Id = new Guid(),
                    Name = "HR",
                    Employees = new List<Employee>
                    {
                        new Employee
                        {
                            Id = new Guid(),
                            Name = "John",
                            Salary = 1000
                        },
                        new Employee
                        {
                            Id = new Guid(),
                            Name = "Jane",
                            Salary = 2000
                        }
                    }
                },
                new Department
                {
                    Id = new Guid(),
                    Name = "IT",
                    Employees = new List<Employee>
                    {
                        new Employee
                        {
                            Id = new Guid(),
                            Name = "Tom",
                            Salary = 3000
                        },
                        new Employee
                        {
                            Id = new Guid(),
                            Name = "Jerry",
                            Salary = 4000
                        }
                    }
                }
            }.AsQueryable();
            var mockSet = new Mock<DbSet<Department>>();
            
            mockSet.As<IAsyncEnumerable<Department>>().Setup(m => m.GetAsyncEnumerator(default))
                .Returns(new TestAsyncEnumerator<Department>(departments.GetEnumerator()));
            mockSet.As<IQueryable<Department>>().Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<Department>(departments.Provider));
            mockSet.As<IQueryable<Department>>().Setup(m => m.Expression).Returns(departments.Expression);
            mockSet.As<IQueryable<Department>>().Setup(m => m.ElementType).Returns(departments.ElementType);
            mockSet.As<IQueryable<Department>>().Setup(m => m.GetEnumerator()).Returns(departments.GetEnumerator());

            // Act
            _context.Setup(x => x.Departments).Returns(mockSet.Object);
            var result = await _departmentRepository.GetDepartmentsWithAverageSalary();
            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal("HR", result.First().Name);
            Assert.Equal(1500, result.First().AverageSalary);
            Assert.Equal("IT", result.Last().Name);
            Assert.Equal(3500, result.Last().AverageSalary);
        }   
    }
}
