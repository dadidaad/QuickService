using QuickServiceWebAPI.DTOs.Change;
using QuickServiceWebAPI.DTOs.Problem;

namespace QuickServiceWebAPI.Services
{
    public interface IProblemService
    {
        public List<ProblemDTO> GetAllProblems();
    }
}
