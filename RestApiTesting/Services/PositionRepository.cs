namespace RestApiTesting.Services
{
    public class PositionRepository : IPositionService
    {
        private readonly RestApiTestingContext _context;
        public PositionRepository(RestApiTestingContext context)
        {
            _context = context;
        }
    }

    public interface IPositionService
    {
    }
}
