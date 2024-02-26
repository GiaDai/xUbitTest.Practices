using Microsoft.EntityFrameworkCore;
using RestApiTesting.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure sqlite database with connection string
builder.Services.AddDbContext<RestApiTestingContext>(options =>
{
    options.UseSqlite("Data Source=RestApiTesting.db");
});

// Register the repository
builder.Services.AddScoped<IWeatherForecastService, WeatherForecastRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentRepository>();
builder.Services.AddScoped<IJobHistoryService, JobHistoryRepository>();
builder.Services.AddScoped<IPositionService, PositionRepository>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
    
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
