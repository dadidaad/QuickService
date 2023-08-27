using AutoMapper;
using QuickServiceWebAPI.DTOs.Dashboard;
using QuickServiceWebAPI.Repositories;
using System.Data.SqlTypes;

namespace QuickServiceWebAPI.Services.Implements
{
    public class DashboardService : IDashboardService
    {
        private readonly IMapper _mapper;
        private readonly IDashboardRepository _repository;
        public DashboardService(IDashboardRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
        public async Task<Dictionary<string, int>> GetRequestTicketByStatusCount()
        {
            var requestTicketCounts = await _repository.GetRequestTicketByStatusCount();
            return requestTicketCounts;
        }

        public async Task<Dictionary<string, int>> GetRequestTicketByPriorityCount()
        {
            var requestTicketCounts = await _repository.GetRequestTicketByPriorityCount();
            return requestTicketCounts;
        }

        public async Task<Dictionary<string, int>> GetRequestTicketByServiceCategoryCount()
        {
            var requestTicketCounts = await _repository.GetRequestTicketByServiceCategoryCount();
            return requestTicketCounts;
        }

        public async Task<Dictionary<string, int>> GetRequestTicketByChangeStatusCount()
        {
            var requestTicketCounts = await _repository.GetRequestTicketByChangeStatusCount();
            return requestTicketCounts;
        }

        public async Task<Dictionary<string, int>> GetRequestTicketByChangeImpactCount()
        {
            var requestTicketCounts = await _repository.GetRequestTicketByChangeImpactCount();
            return requestTicketCounts;
        }

        public async Task<Dictionary<string, int>> GetRequestTicketByProblemStatusCount()
        {
            var requestTicketCounts = await _repository.GetRequestTicketByProblemStatusCount();
            return requestTicketCounts;
        }

        public async Task<Dictionary<string, int>> GetRequestTicketByProblemPriorityCount()
        {
            var requestTicketCounts = await _repository.GetRequestTicketByProblemPriorityCount();
            return requestTicketCounts;
        }

        public async Task<Dictionary<string, int>> GetRequestTicketByProblemImpactCount()
        {
            var requestTicketCounts = await _repository.GetRequestTicketByProblemImpactCount();
            return requestTicketCounts;
        }

        public async Task<List<CountRequestTicketByDayDTO>> CountRequestTicketByDay(CountByDayDTO countByDayDTO)
        {
            if (!countByDayDTO.FromDate.HasValue)
            {
                countByDayDTO.FromDate = SqlDateTime.MinValue.Value;
            }
            if (!countByDayDTO.ToDate.HasValue)
            {
                countByDayDTO.ToDate = DateTime.Now;
            }

            var resultListFromDb = new List<dynamic>();
            if (!countByDayDTO.NeedDividedByServiceItem)
            {
                resultListFromDb = await _repository
                    .CountRequestTicketByDay(countByDayDTO.FromDate.Value, countByDayDTO.ToDate.Value);
            }
            else
            {
                resultListFromDb = await _repository
                    .CountRequestTicketByDayAndServiceItem(countByDayDTO.FromDate.Value, countByDayDTO.ToDate.Value);
            }
            return resultListFromDb.Select(x => new CountRequestTicketByDayDTO
            {
                Date = x.Date,
                TotalCreated = x.TotalCreated,
                TotalResolved = x.TotalResolved,
                ServiceItemName = x.ServiceItemName is DBNull ? null : x.ServiceItemName
            }).ToList();
        }
    }
}
