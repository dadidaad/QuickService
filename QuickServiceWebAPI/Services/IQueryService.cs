using QuickServiceWebAPI.DTOs.RequestTicket;

namespace QuickServiceWebAPI.Services
{
    public interface IQueryService 
    {
        public List<RequestTicketDTO> GetQueryRequestTicket(string? assignee, DateTime? createFrom, DateTime? createTo, string? description,
                                                         string? group, string? requester, string? requestType, string? priority, string? status);
    }
}
