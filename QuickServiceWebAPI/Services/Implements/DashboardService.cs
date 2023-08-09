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

        public async Task<Dictionary<string, int>> GetRequestTicketByServiceCategoryCount()
        {
            var requestTicketCounts = await _repository.GetRequestTicketByServiceCategoryCount();

                    
            return requestTicketCounts;
        }
    }
}
