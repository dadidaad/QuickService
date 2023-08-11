using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Repositories
{
    public interface ISlametricRepository
    {
        public List<Slametric> GetSLAmetrics();
        public Task<Slametric> GetSLAmetricById(string slametricId);
        public Task AddSLAmetric(Slametric slametric);
        public Task UpdateSLAmetric(Slametric slametric);
        public Task DeleteSLAmetric(Slametric slametric);
        public Task<Slametric> GetLastSLAmetric();
        public Task DeleteSlaMetricsOfSla(Sla sla);

    }
}
