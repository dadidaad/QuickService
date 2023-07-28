using QuickServiceWebAPI.DTOs.Change;

namespace QuickServiceWebAPI.Services
{
    public interface IChangeService
    {
        public Task CreateChange(CreateChangeDTO createChangeDTO);

    }
}
