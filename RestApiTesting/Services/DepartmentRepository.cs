using Microsoft.EntityFrameworkCore;
using RestApiTesting.Models;

namespace RestApiTesting.Services
{
    public class DepartmentRepository : IDepartmentService
    {
        private readonly RestApiTestingContext _context;
        public DepartmentRepository(RestApiTestingContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetDepartmentsWithEmployeesBetween30And40()
        {
            return await _context.Departments
                .Include(x => x.Employees)
                .Where(x => x.Employees.Any(e => e.Age >= 30 && e.Age <= 40))
                .ToListAsync();
        }

        public async Task<IEnumerable<Department>> GetDepartmentsWithEmployeesLevelLessThan3()
        {
            return await _context.Departments
                .Include(x => x.Employees)
                .Where(x => x.Employees.Any(e => e.Level < 3))
                .ToListAsync();
        }

        public async Task<IEnumerable<Department>> GetDepartmentsWithEmployeesLevelGreaterThan3()
        {
            return await _context.Departments
                .Include(x => x.Employees)
                .Where(x => x.Employees.Any(e => e.Level > 3))
                .ToListAsync();
        }

        public async Task<IEnumerable<Department>> GetDepartmentsWithAverageSalary()
        {
            return await _context.Departments
                .Include(x => x.Employees)
                .Select(x => new Department
                {
                    Id = x.Id,
                    Name = x.Name,
                    Employees = x.Employees,
                    AverageSalary = x.Employees.Average(e => e.Salary)
                })
                .ToListAsync();
        }
    }

    public interface IDepartmentService
    {
        // Get department has employees aged between 30 and 40
        Task<IEnumerable<Department>> GetDepartmentsWithEmployeesBetween30And40();

        // Get department has employees level less than 3
        Task<IEnumerable<Department>> GetDepartmentsWithEmployeesLevelLessThan3();

        // Get department has employees level greater than 3
        Task<IEnumerable<Department>> GetDepartmentsWithEmployeesLevelGreaterThan3();

        // Compute the average salary of all employees for each department
        Task<IEnumerable<Department>> GetDepartmentsWithAverageSalary();
    }
}
