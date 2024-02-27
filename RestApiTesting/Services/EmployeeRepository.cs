using Microsoft.EntityFrameworkCore;
using RestApiTesting.Models;

namespace RestApiTesting.Services
{
    public class EmployeeRepository : IEmployeeService
    {
        private readonly RestApiTestingContext _context;

        public EmployeeRepository(RestApiTestingContext context)
        {
            _context = context;
        }

        public async Task<Employee> GetEmployeeById(Guid id)
        {
            return await _context.Employees.FindAsync(id);
        }
        public void CreateEmployee(Employee employee)
        {
            if(employee is not null && _context is not null){
                _context.Employees.Add(employee);
                _context.SaveChanges();
            }
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<double> GetAverageAge()
        {
            var employees = await _context.Employees.ToListAsync();
            return employees.Average(x => x.Age);
        }

        public async Task<int> GetMaxAge()
        {
            var employees = await _context.Employees.ToListAsync();
            return employees.Max(x => x.Age);
        }

        public async Task<int> GetMinAge()
        {
            var employees = await _context.Employees.ToListAsync();
            return employees.Min(x => x.Age);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByNameAndAge()
        {
            return await _context.Employees.Where(x => x.Name.StartsWith("J") && x.Age > 30).ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByDepartment(Guid departmentId)
        {
            return await _context.Employees.Where(x => x.DepartmentId == departmentId).ToListAsync();
        }

        public async Task<IEnumerable<IGrouping<Guid, Employee>>> GetEmployeesGroupedByDepartment()
        {
            return await _context.Employees.GroupBy(x => x.DepartmentId).ToListAsync();
        }
    }

    public interface IEmployeeService
    {
        // Get employee by id method
        Task<Employee> GetEmployeeById(Guid id);

        // Create a new employee method
        void CreateEmployee(Employee employee);

        // Generate Get all employees method
        Task<IEnumerable<Employee>> GetAllEmployees();

        // Get average age of employees
        Task<double> GetAverageAge();

        // Get maximum age of employees
        Task<int> GetMaxAge();

        // Get minimum age of employees
        Task<int> GetMinAge();

        // Get employees by name start with J and age greater than 30
        Task<IEnumerable<Employee>> GetEmployeesByNameAndAge();

        // Get employees by department
        Task<IEnumerable<Employee>> GetEmployeesByDepartment(Guid departmentId);

        // Get employees grouped by department
        Task<IEnumerable<IGrouping<Guid, Employee>>> GetEmployeesGroupedByDepartment();

    }
}
