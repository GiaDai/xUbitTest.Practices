using Microsoft.EntityFrameworkCore;
using RestApiTesting.Models;

public class RestApiTestingContext : DbContext
{
    public RestApiTestingContext()
    {
        
    }
    public RestApiTestingContext(DbContextOptions<RestApiTestingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<WeatherForecast> WeatherForecasts { get; set; }
    public virtual DbSet<Employee> Employees { get; set; }
    public virtual DbSet<Department> Departments { get; set; }
    public virtual DbSet<Position> Positions { get; set; }
    public virtual DbSet<JobHistory> JobHistories { get; set; }
}