using QuickServiceWebAPI.DTOs.Change;
using QuickServiceWebAPI.DTOs.Query;
using QuickServiceWebAPI.DTOs.RequestTicket;

namespace QuickServiceWebAPI.Services
{
    public interface IChangeService
    {
        public Task<ChangeDTO> CreateChange(CreateChangeDTO createChangeDTO);
        public Task<ChangeDTO> UpdateChange(UpdateChangeDTO updateChangeDTO);
        public Task<List<ChangeDTO>> GetAllChanges();
        public Task<ChangeDTO> GetChange(string changeId);
        public Task DeleteChange(string changeId);
        public Task<List<TicketQueryAdminDTO>> GetRequestTicketsQueryAdmin(QueryDTO queryDto);
    }
}
