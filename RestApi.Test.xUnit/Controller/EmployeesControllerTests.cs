using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RestApiTesting.Controllers;
using RestApiTesting.Feartures.Employees.Queries;
using RestApiTesting.Models;
using RestApiTesting.Services;

namespace RestApi.Test.xUnit.Controller
{
    public class EmployeesControllerTests
    {
        private readonly Mock<IMediator> _mediator = new Mock<IMediator>();
        private readonly Mock<IEmployeeService> _employeeService;
        private readonly EmployeesController _employeesController;
        public EmployeesControllerTests()
        {
            _employeeService = new Mock<IEmployeeService>();
            _employeesController = new EmployeesController(_mediator.Object,_employeeService.Object);
        }

        // Write test for Index Action Method return exact number of employees
        [Fact]
        public async Task GetAllEmployees_ReturnsExactNumberOfEmployees()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee { Id = new Guid(), Name = "John", AccountNumber = "123456" },
                new Employee { Id = new Guid(), Name = "Jane", AccountNumber = "123457" }
            };
            _employeeService.Setup(x => x.GetAllEmployees()).ReturnsAsync(employees);

            // Act
            var result = await _employeesController.GetAllEmployees();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnEmployees = Assert.IsType<List<Employee>>(okResult.Value);
            Assert.Equal(2, returnEmployees.Count);
        }

        //Write test for CreateEmployee Action Method returns 201 Created
        [Fact]
        public void CreateEmployee_Returns201Created()
        {
            // Arrange
            var employee = new Employee { Id = new Guid(), Name = "John", AccountNumber = "255-8547963214-41" };

            // Act
            var result = _employeesController.CreateEmployee(employee);
            
            // Assert
            var createdResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(201, createdResult.StatusCode);
        }

        //Write test for CreateEmployee Action Method invalid model returns 400 BadRequest
        // Name is not provided
        [Fact]
        public void CreateEmployee_InvalidModel_Returns400BadRequest()
        {
            // Arrange
            _employeesController.ModelState.AddModelError("Name", "Name is required");
            var employee = new Employee { Id = new Guid(), AccountNumber = "255-8547963214-41" };

            // Act
            var result = _employeesController.CreateEmployee(employee);

            // Assert
            var badRequestResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        //write one additional test to verify that the CreateEmployee method, from our repository, never executes if the model state is invalid
        [Fact]
        public void CreateEmployee_InvalidModel_NeverExecutesCreateEmployee()
        {
            // Arrange
            _employeesController.ModelState.AddModelError("Name", "Name is required");
            var employee = new Employee { Id = new Guid(), AccountNumber = "255-8547963214-41" };

            // Act
            _employeesController.CreateEmployee(employee);

            // Assert
            _employeeService.Verify(x => x.CreateEmployee(It.IsAny<Employee>()), Times.Never);
        }

        // write test if model is valid should be excuted just once
        [Fact]
        public void CreateEmployee_ValidModel_ExecutesCreateEmployeeOnce()
        {
            Employee? emp = null;
            // Mock
            _employeeService.Setup(x => x.CreateEmployee(It.IsAny<Employee>()))
                .Callback<Employee>(x => emp = x);
            // Arrange
            var employee = new Employee { 
                Id = new Guid(), 
                Name = "John", 
                Age = 25,
                AccountNumber = "255-8547963214-41" 
            };

            // Act
            _employeesController.CreateEmployee(employee);

            // Assert
            _employeeService.Verify(x => x.CreateEmployee(It.IsAny<Employee>()), Times.Once);

            Assert.Equal(employee.Name, emp.Name);
            Assert.Equal(employee.AccountNumber, emp.AccountNumber);
            Assert.Equal(employee.Age, emp.Age);
        }

        // write test for GetEmployeeById Action Method returns 200 OK
        [Fact]
        public async Task GetEmployeeById_Returns200OK()
        {
            var idGuid = new Guid("e035daec-a259-4037-b66a-7d3ceab5637a");
            var departmentGui = new Guid("e035daec-a259-4037-b66a-7d3ceab5637b");
            // Arrange
            var employee = new Employee { 
                Id = idGuid, 
                Name = "John", 
                AccountNumber = "255-8547963214-41", 
                DepartmentId =  departmentGui,
                Age = 25,
                JobHistories = null
            };
            _mediator.Setup(x => x.Send(It.IsAny<GetEmployeeByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(employee);
            _employeeService.Setup(x => x.GetEmployeeById(It.IsAny<Guid>())).ReturnsAsync(employee);

            // Act
            var result = await _employeesController.GetEmployeeById(idGuid);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnEmployee = Assert.IsType<Employee>(okResult.Value);
            Assert.Equal(employee.Name, returnEmployee.Name);
            Assert.Equal(employee.AccountNumber, returnEmployee.AccountNumber);
        }

        // write test for GetEmployeeById Action Method returns 404 NotFound
        [Fact]
        public async Task GetEmployeeById_Returns404NotFound()
        {
            // Arrange
            var idGuid = new Guid("e035daec-a259-4037-b66a-7d3ceab5637a");
            _mediator.Setup(x => x.Send(It.IsAny<GetEmployeeByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync((Employee?)null);
            _employeeService.Setup(x => x.GetEmployeeById(It.IsAny<Guid>())).ReturnsAsync((Employee?)null);

            // Act
            var result = await _employeesController.GetEmployeeById(idGuid);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        
    }
}
