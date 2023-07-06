using AutoMapper;
using QuickServiceWebAPI.DTOs.SLAMetric;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class SlametricService : ISlametricService
    {
        private readonly ISlametricRepository _repository;
        private readonly IMapper _mapper;
        public SlametricService(ISlametricRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<SlametricDTO> GetSLAmetrics()
        {
            var slametrics = _repository.GetSLAmetrics();
            return slametrics.Select(slametric => _mapper.Map<SlametricDTO>(slametric)).ToList();
        }

        public async Task<SlametricDTO> GetSLAmetricById(string slametricId)
        {
            var slametric = await _repository.GetSLAmetricById(slametricId);
            return _mapper.Map<SlametricDTO>(slametric);
        }

        public async Task CreateSLAmetric(CreateUpdateSlametricDTO createUpdateSlametricDTO)
        {
            var slametric = _mapper.Map<Slametric>(createUpdateSlametricDTO);
            slametric.SlametricId = await GetNextId();
            await _repository.AddSLAmetric(slametric);
        }

        public async Task UpdateSLAmetric(string slametricId, CreateUpdateSlametricDTO createUpdateSlametricDTO)
        {
            Slametric slametric = await _repository.GetSLAmetricById(slametricId);
            if (slametric == null)
            {
                throw new AppException("Slametric not found");
            }
            if (!String.IsNullOrEmpty(createUpdateSlametricDTO.Piority))
            {
                slametric.Piority = createUpdateSlametricDTO.Piority;
            }
            if (!String.IsNullOrEmpty(createUpdateSlametricDTO.EscalationPolicy))
            {
                slametric.EscalationPolicy = createUpdateSlametricDTO.EscalationPolicy;
            }
            if (!String.IsNullOrEmpty(createUpdateSlametricDTO.NotificationRules))
            {
                slametric.NotificationRules = createUpdateSlametricDTO.NotificationRules;
            }
            if (!String.IsNullOrEmpty(createUpdateSlametricDTO.BusinessHourId))
            {
                slametric.BusinessHourId = createUpdateSlametricDTO.BusinessHourId;
            }
            if (!String.IsNullOrEmpty(createUpdateSlametricDTO.Slaid))
            {
                slametric.Slaid = createUpdateSlametricDTO.Slaid;
            }
            slametric = _mapper.Map<CreateUpdateSlametricDTO, Slametric>(createUpdateSlametricDTO, slametric);
            await _repository.UpdateSLAmetric(slametric);
        }

        public async Task DeleteSLAmetric(string slametricId)
        {

        }
        public async Task<string> GetNextId()
        {
            Slametric lastSlametric = await _repository.GetLastSLAmetric();
            int id = 0;
            if (lastSlametric == null)
            {
                id = 1;
            }
            else
            {
                id = IDGenerator.ExtractNumberFromId(lastSlametric.SlametricId) + 1;
            }
            string slametricId = IDGenerator.GenerateSlametricId(id);
            return slametricId;
        }
    }
}
