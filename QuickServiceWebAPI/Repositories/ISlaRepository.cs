using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface ISlaRepository
    {
        public List<Sla> GetSLAs();
        public Task<Sla> GetSLAById(string slaId);
        public Task AddSLA(Sla sla);
        public Task UpdateSLA(Sla sla);
        public Task DeleteSLA(Sla sla);
        public Task<Sla> GetLastSLA();
        public Task<Sla> GetDefaultSla();
    }
}
