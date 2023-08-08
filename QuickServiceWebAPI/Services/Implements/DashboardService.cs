using AutoMapper;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Repositories.Implements;

namespace QuickServiceWebAPI.Services.Implements
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _repository;
        public DashboardService(IDashboardRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> GetChangeCount()
        {
            return await _repository.GetChangeCount();
        }

        public async Task<int> GetProblemCount()
        {
            return await _repository.GetProblemCount();
        }

        public async Task<int> GetRequestTicketIncidentCount()
        {
            return await _repository.GetRequestTicketIncidentCount();
        }

        public async Task<int> GetRequestTicketCount()
        {
            return await _repository.GetRequestTicketCount();
        }

        public async Task<List<Dictionary<string, int>>> GetRequestTicketByServiceCategoryCount()
        {
            List<int> requestTicketCounts = await _repository.GetRequestTicketByServiceCategoryCount();

            var result = new List<Dictionary<string, int>>();

            for (int i = 0; i < requestTicketCounts.Count; i++)
            {
                var propertyName = $"SECA{i + 1}";
                var propertyValue = requestTicketCounts[i];

                var propertyObject = new Dictionary<string, int>
                {
                    { propertyName, propertyValue }
                };
                result.Add(propertyObject);
            }

            return result;
        }
    }
}
