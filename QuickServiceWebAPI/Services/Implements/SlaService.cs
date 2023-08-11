using AutoMapper;
using QuickServiceWebAPI.DTOs.Sla;
using QuickServiceWebAPI.DTOs.SLAMetric;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class SlaService : ISlaService
    {
        private readonly ISlaRepository _repository;
        private readonly ISlametricService _slametricService;
        private readonly ISlametricRepository _slametricRepository;
        private readonly IMapper _mapper;
        public SlaService(ISlaRepository repository, IMapper mapper
            , ISlametricService slametricService, ISlametricRepository slametricRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _slametricService = slametricService;
            _slametricRepository = slametricRepository;
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

        public async Task<SlaDTO> CreateSLA(CreateSlaDTO createSlaDTO)
        {
            var sla = _mapper.Map<Sla>(createSlaDTO);
            sla.Slaid = await GetNextId();
            var defaultSla = await _repository.GetDefaultSla();
            List<Slametric> slametrics = EnumerableUtils.DeepCopy(defaultSla.Slametrics.ToList());
            using (var slaMetrics = slametrics.GetEnumerator())
            {
                string currentId = await _slametricService.GetNextId();
                if (slaMetrics.MoveNext())
                {
                    slaMetrics.Current.SlametricId = currentId;
                    slaMetrics.Current.Slaid = sla.Slaid;
                }
                while (slaMetrics.MoveNext())
                {
                    var nextId = GenerateNextIdForSlaMetrics(currentId);
                    var slaMetric = slaMetrics.Current;
                    slaMetric.SlametricId = nextId;
                    slaMetric.Slaid = sla.Slaid;
                    currentId = nextId;
                }
            }
            sla.Slametrics = slametrics;
            var slaAdded = await _repository.AddSLA(sla);
            if(slaAdded == null)
            {
                throw new AppException($"Create failed");
            }
            return _mapper.Map<SlaDTO>(slaAdded);
        }

        public async Task UpdateSLA(UpdateSlaDTO updateSlaDTO)
        {
            Sla sla = await _repository.GetSLAById(updateSlaDTO.SlaId);
            if (sla == null)
            {
                throw new AppException("Sla not found");
            }
            updateSlaDTO.Slametrics.ForEach(slam => slam.SlaId = updateSlaDTO.SlaId);
            sla = _mapper.Map(updateSlaDTO, sla);
            await _repository.UpdateSLA(sla);
        }

        public async Task DeleteSLA(string slaId)
        {
            var sla = await _repository.GetSLAById(slaId);
            if(sla == null)
            {
                throw new AppException($"Sla with id {slaId} not found");
            }
            await _slametricRepository.DeleteSlaMetricsOfSla(sla);
            await _repository.DeleteSLA(sla);
        }

        private string GenerateNextIdForSlaMetrics(string currentId)
        {
            int id = IDGenerator.ExtractNumberFromId(currentId) + 1;
            string slaId = IDGenerator.GenerateSlametricId(id);
            return slaId;
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
