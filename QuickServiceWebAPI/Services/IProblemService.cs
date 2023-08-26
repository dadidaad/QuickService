using QuickServiceWebAPI.DTOs.BusinessHour;
using QuickServiceWebAPI.DTOs.Change;
using QuickServiceWebAPI.DTOs.Problem;
using QuickServiceWebAPI.DTOs.Query;
using QuickServiceWebAPI.DTOs.RequestTicket;

namespace QuickServiceWebAPI.Services
{
    public interface IProblemService
    {
        public List<ProblemDTO> GetAllProblems();
        public Task<ProblemDTO> GetProblemById(string problemId);
        public Task<ProblemDTO> CreateProblem(CreateProblemDTO createProblemDTO);
        public Task UpdateProblem(UpdateProblemDTO updateProblemDTO);

        //public Task<List<TicketQueryAdminDTO>> GetRequestTicketsQueryAdmin(QueryDTO queryDto);
    }
}
