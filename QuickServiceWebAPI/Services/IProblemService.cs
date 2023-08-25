using QuickServiceWebAPI.DTOs.Change;
using QuickServiceWebAPI.DTOs.Problem;
using QuickServiceWebAPI.DTOs.Query;
using QuickServiceWebAPI.DTOs.RequestTicket;

namespace QuickServiceWebAPI.Services
{
    public interface IProblemService
    {
        public List<ProblemDTO> GetAllProblems();
        public Task<List<TicketQueryAdminDTO>> GetRequestTicketsQueryAdmin(QueryDTO queryDto);
    }
}
