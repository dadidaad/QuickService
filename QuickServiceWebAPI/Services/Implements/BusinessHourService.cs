using AutoMapper;
using QuickServiceWebAPI.DTOs.BusinessHour;
using QuickServiceWebAPI.DTOs.WorkflowStep;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class BusinessHourService : IBusinessHourService
    {
        private readonly IBusinessHourRepository _repository;
        private readonly IMapper _mapper;
        public BusinessHourService(IBusinessHourRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<BusinessHourDTO> GetBusinessHours()
        {
            var businessHours = _repository.GetBusinessHours();
            return businessHours.Select(businessHour => _mapper.Map<BusinessHourDTO>(businessHour)).ToList();
        }

        public async Task<BusinessHourDTO> GetBusinessHourById(string businessHourId)
        {
            var businessHour = await _repository.GetBusinessHourById(businessHourId);
            return _mapper.Map<BusinessHourDTO>(businessHour);
        }

        public async Task CreateBusinessHour(CreateUpdateBusinessHourDTO createUpdateBusinessHourDTO)
        {
            var businessHour = _mapper.Map<BusinessHour>(createUpdateBusinessHourDTO);
            businessHour.BusinessHourId = await GetNextId();
            await _repository.AddBusinessHour(businessHour);
        }

        public async Task UpdateBusinessHour(string businessHourId, CreateUpdateBusinessHourDTO createUpdateBusinessHourDTO)
        {
            BusinessHour businessHour = await _repository.GetBusinessHourById(businessHourId);
            if (businessHour == null)
            {
                throw new AppException("BusinessHour not found");
            }
            businessHour = _mapper.Map(createUpdateBusinessHourDTO, businessHour);
            await _repository.UpdateBusinessHour(businessHour);
        }

        public async Task DeleteBusinessHour(string businessHourId)
        {
            var businessHour = await _repository.GetBusinessHourById(businessHourId);
            await _repository.DeleteBusinessHour(businessHour);
        }
        public async Task<string> GetNextId()
        {
            BusinessHour lastBusinessHour = await _repository.GetLastBusinessHour();
            int id = 0;
            if (lastBusinessHour == null)
            {
                id = 1;
            }
            else
            {
                id = IDGenerator.ExtractNumberFromId(lastBusinessHour.BusinessHourId) + 1;
            }
            string businessHourId = IDGenerator.GenerateBusinessHourId(id);
            return businessHourId;
        }
    }
}
