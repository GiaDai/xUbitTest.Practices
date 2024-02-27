using MediatR;
using Microsoft.AspNetCore.Mvc;
using RestApiTesting.Feartures.Employees.Queries;
using RestApiTesting.Models;
using RestApiTesting.Services;
using RestApiTesting.Validation;

namespace RestApiTesting.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IEmployeeService _employeeRepository;
        private readonly AccountNumberValidation _accountNumberValidation;
        public EmployeesController(
            IMediator mediator,
            IEmployeeService employeeRepository)
        {
            _mediator = mediator;
            _accountNumberValidation = new AccountNumberValidation();
            _employeeRepository = employeeRepository;
        }

        [HttpPost]
        public IActionResult CreateEmployee([FromBody] Employee employee)
        {
            // check employee is not valid
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid employee");
            if (!_accountNumberValidation.IsValid(employee.AccountNumber))
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid account number");

            _employeeRepository.CreateEmployee(employee);
            return StatusCode(StatusCodes.Status201Created, "Employee created");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllEmployees();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            var employee = await _mediator.Send(new GetEmployeeByIdQuery { Id = id });
            if (employee == null)
                return NotFound();
            
            return Ok(employee);
        }
    }
}
