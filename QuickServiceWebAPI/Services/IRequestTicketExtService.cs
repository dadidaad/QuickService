using QuickServiceWebAPI.DTOs.Group;
using QuickServiceWebAPI.DTOs.RequestTicketExt;

namespace QuickServiceWebAPI.Services
{
    public interface IRequestTicketExtService
    {
        public List<RequestTicketExtDTO> GetRequestTicketExts();
        public Task<RequestTicketExtDTO> GetRequestTicketExtById(string requestTicketExtId);
        public Task CreateRequestTicketExt(CreateUpdateRequestTicketExtDTO createUpdateRequestTicketExtDTO);
        public Task UpdateRequestTicketExt(string requestTicketExtId, CreateUpdateRequestTicketExtDTO createUpdateRequestTicketExtDTO);
        public Task DeleteRequestTicketExt(string requestTicketExtId);
        public Task<string> GetNextId();
    }
}
