using System.ComponentModel.DataAnnotations;

namespace RestApiTesting.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Age is required")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Account number is required")]
        public string? AccountNumber { get; set; }
        // Level of employee with 1 being the lowest and 5 being the highest
        public int Level { get; set; }
        // Salary of employee
        public decimal Salary { get; set; }
        // Employee has a department
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
        // Employee has many job histories
        public ICollection<JobHistory> JobHistories { get; set; }
    }
}
