using Moq;
using RestApiTesting.Models;
using RestApiTesting.Services;

namespace RestApi.Test.xUnit.Repository
{
    public class JobHistoryRepositoryTest
    {
        private Mock<RestApiTestingContext> _context = new Mock<RestApiTestingContext>();
        private JobHistoryRepository _jobHistoryRepository;

        public JobHistoryRepositoryTest()
        {
            _jobHistoryRepository = new JobHistoryRepository(_context.Object);
        }
    }
}
