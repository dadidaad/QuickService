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
        private readonly IBusinessHourRepository _businessHourRepository;
        private readonly ISlaRepository _slaRepository;
        private readonly IMapper _mapper;
        public SlametricService(ISlametricRepository repository, IBusinessHourRepository businessHourRepository, ISlaRepository slaRepository, IMapper mapper)
        {
            _repository = repository;
            _businessHourRepository = businessHourRepository;
            _slaRepository = slaRepository;
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
            if (slametric == null)
            {
                throw new AppException("Slametric not found");
            }
            return _mapper.Map<SlametricDTO>(slametric);
        }

        //public async Task CreateSLAmetrics(CreateSlametricDTO createSlametricDTO)
        //{
        //    var sla = _slaRepository.GetSLAById(createSlametricDTO.Slaid);
        //    if(sla == null)
        //    {
        //        throw new AppException("SLA with id " + createSlametricDTO.Slaid + " not found");
        //    }
        //    var slametrics = _mapper.Map<CreateSlametricDTO, IEnumerable<Slametric>>(createSlametricDTO).ToList();
        //    foreach(var slametric in slametrics)
        //    {
        //        slametric.SlametricId = await GetNextId();
        //        await _repository.AddSLAmetric(slametric);
        //    }

        //}

        public async Task UpdateSLAmetric(UpdateSlametricsDTO updateSlametricsDTO)
        {
            Slametric slametric = await _repository.GetSLAmetricById(updateSlametricsDTO.SlametricId);
            if (slametric == null)
            {
                throw new AppException("Slametric not found");
            }
            //if (_businessHourRepository.GetBusinessHourById(updateSlametricsDTO.BusinessHourId) == null)
            //{
            //    throw new AppException("Business hour with id " + createUpdateSlametricDTO.BusinessHourId + " not found");
            //}
            slametric = _mapper.Map(updateSlametricsDTO, slametric);
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
