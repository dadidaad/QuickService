using AutoMapper;
using QuickServiceWebAPI.DTOs.Sla;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class SlaService : ISlaService
    {
        private readonly ISlaRepository _repository;
        private readonly IMapper _mapper;
        public SlaService(ISlaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<SlaDTO> GetSLAs()
        {
            var slas = _repository.GetSLAs();
            return slas.Select(sla => _mapper.Map<SlaDTO>(sla)).ToList();
        }

        public async Task<SlaDTO> GetSlaById(string slaId)
        {
            var sla = await _repository.GetSLAById(slaId);
            return _mapper.Map<SlaDTO>(sla);
        }

        public async Task CreateSLA(CreateUpdateSlaDTO createUpdateSlaDTO)
        {
            var sla = _mapper.Map<Sla>(createUpdateSlaDTO);
            sla.Slaid = await GetNextId();
            await _repository.AddSLA(sla);
        }

        public async Task UpdateSLA(string slaId, CreateUpdateSlaDTO createUpdateSlaDTO)
        {
            Sla sla = await _repository.GetSLAById(slaId);
            if (sla == null)
            {
                throw new AppException("Sla not found");
            }
            sla = _mapper.Map(createUpdateSlaDTO, sla);
            await _repository.UpdateSLA(sla);
        }

        public async Task DeleteSLA(string slaId)
        {
            var sla = await _repository.GetSLAById(slaId);
            await _repository.DeleteSLA(sla);
        }
        public async Task<string> GetNextId()
        {
            Sla lastSla = await _repository.GetLastSLA();
            int id = 0;
            if (lastSla == null)
            {
                id = 1;
            }
            else
            {
                id = IDGenerator.ExtractNumberFromId(lastSla.Slaid) + 1;
            }
            string slaId = IDGenerator.GenerateSlaId(id);
            return slaId;
        }
    }
}
