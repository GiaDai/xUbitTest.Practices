using MediatR;
using RestApiTesting.Models;
using RestApiTesting.Services;
using System.Runtime.Serialization;

namespace RestApiTesting.Feartures.Employees.Queries
{
    
    public class GetEmployeeByIdQuery : IRequest<Employee>
    {
        public Guid Id { get; set; }
        public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Employee>
        {
            private readonly IEmployeeService _employeeService;
            public GetEmployeeByIdQueryHandler(IEmployeeService employeeService)
            {
                _employeeService = employeeService;
            }
            public async Task<Employee> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
            {
                var employee = await _employeeService.GetEmployeeById(request.Id);
                return employee;
            }
        }
    }

    [Serializable]
    internal class NotFoundException : Exception
    {
        private string v;
        private int id;

        public NotFoundException()
        {
        }

        public NotFoundException(string? message) : base(message)
        {
        }

        public NotFoundException(string v, int id)
        {
            this.v = v;
            this.id = id;
        }

        public NotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
