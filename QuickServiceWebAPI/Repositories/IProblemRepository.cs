﻿using QuickServiceWebAPI.DTOs.Query;
using QuickServiceWebAPI.DTOs.RequestTicket;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IProblemRepository
    {
        public List<Problem> GetProblems();
        public Task<Problem> GetProblemById(string problemId);
        public Task AddProblem(Problem problem);
        public Task UpdateProblem(Problem problem);
        public Task DeleteProblem(Problem problem);
        public Task<Problem> GetLastProblem();
        public Task<List<TicketQueryAdminDTO>> GetRequestTicketsQueryAdmin(QueryDTO queryDto);
    }
}
