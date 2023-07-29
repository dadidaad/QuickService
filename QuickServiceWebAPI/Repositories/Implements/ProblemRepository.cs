using Microsoft.EntityFrameworkCore;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories.Implements
{
    public class ProblemRepository : IProblemRepository
    {
        private readonly QuickServiceContext _context;
        private readonly ILogger<ProblemRepository> _logger;

        public ProblemRepository(QuickServiceContext context, ILogger<ProblemRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddProblem(Problem problem)
        {
            try
            {
                _context.Problems.Add(problem);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<Problem> GetProblemById(string problemId)
        {
            try
            {
                return await _context.Problems.FindAsync(problemId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public List<Problem> GetProblems()
        {
            try
            {
                return _context.Problems.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task UpdateProblem(Problem problem)
        {
            try
            {
                _context.Problems.Update(problem);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task DeleteProblem(Problem problem)
        {
            try
            {
                _context.Problems.Remove(problem);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw;
            }
        }

        public async Task<Problem> GetLastProblem()
        {
            try
            {
                return await _context.Problems.OrderByDescending(u => u.ProblemId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                throw; // Rethrow the exception to propagate it up the call stack if necessary
            }
        }
    }
}
