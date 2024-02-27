using Microsoft.EntityFrameworkCore;
using RestApiTesting.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MediatR;
using RestApiTesting.Behavior;

var builder = WebApplication.CreateBuilder(args);
var _config = builder.Configuration;
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

// Register jwt authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _config["Jwt:Issuer"],
            ValidAudience = _config["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]))
        };
    });

// Add services to the container.
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
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
app.UseAuthentication();
app.MapControllers();

app.Run();
