using QuickServiceWebAPI.DTOs.Query;
using QuickServiceWebAPI.DTOs.RequestTicket;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface IChangeRepository
    {
        public Task<List<Change>> GetChanges();
        public Task<Change> GetChangeById(string changeId);
        public Task AddChange(Change change);
        public Task UpdateChange(Change change);
        public Task DeleteChange(Change change);
        public Task<Change> GetLastChange();
        //public Task<List<TicketQueryAdminDTO>> GetRequestTicketsQueryAdmin(QueryDTO queryDto);
    }
}
