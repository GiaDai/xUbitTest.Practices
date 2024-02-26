using Microsoft.AspNetCore.Mvc;
using RestApiTesting.Models;
using RestApiTesting.Services;
using RestApiTesting.Validation;

namespace RestApiTesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeRepository;
        private readonly AccountNumberValidation _accountNumberValidation;
        public EmployeesController(IEmployeeService employeeRepository)
        {
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
    }
}
