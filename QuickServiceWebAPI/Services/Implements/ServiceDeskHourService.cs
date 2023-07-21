using AutoMapper;
using QuickServiceWebAPI.DTOs.ServiceDeskHour;
using QuickServiceWebAPI.DTOs.ServiceItem;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Repositories.Implements;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class ServiceDeskHourService : IServiceDeskHourService
    {
        private readonly IServiceDeskHourRepository _repository;
        private readonly IBusinessHourRepository _businessHourRepository;
        private readonly IMapper _mapper;
        public ServiceDeskHourService(IServiceDeskHourRepository repository, IBusinessHourRepository businessHourRepository, IMapper mapper)
        { 
            _businessHourRepository = businessHourRepository;
            _repository = repository;
            _mapper = mapper;
        }

        public List<ServiceDeskHourDTO> GetServiceDeskHours()
        {
            var serviceDeskHours = _repository.GetServiceDeskHours();
            return serviceDeskHours.Select(serviceDeskHour => _mapper.Map<ServiceDeskHourDTO>(serviceDeskHour)).ToList();
        }

        public async Task<ServiceDeskHourDTO> GetServiceDeskHourById(string serviceDeskHourId)
        {
            var serviceDeskHour = await _repository.GetServiceDeskHourById(serviceDeskHourId);
            return _mapper.Map<ServiceDeskHourDTO>(serviceDeskHour);
        }

        public async Task CreateServiceDeskHour(CreateUpdateServiceDeskHourDTO createUpdateServiceDeskHourDTO)
    {
            var serviceDeskHour = _mapper.Map<ServiceDeskHour>(createUpdateServiceDeskHourDTO);
            serviceDeskHour.ServiceDeskHourId = await GetNextId();
            await _repository.AddServiceDeskHour(serviceDeskHour);
        }

        public async Task UpdateServiceDeskHour(string serviceDeskHourId, CreateUpdateServiceDeskHourDTO createUpdateServiceDeskHourDTO)
    {
            ServiceDeskHour serviceDeskHour = await _repository.GetServiceDeskHourById(serviceDeskHourId);
            if (serviceDeskHour == null)
            {
                throw new AppException("ServiceDeskHour not found");
            }
            if (_businessHourRepository.GetBusinessHourById(createUpdateServiceDeskHourDTO.BusinessHourId) == null)
            {
                throw new AppException("Business hour with id " + createUpdateServiceDeskHourDTO.BusinessHourId + " not found");
            }
            serviceDeskHour = _mapper.Map(createUpdateServiceDeskHourDTO, serviceDeskHour);
            await _repository.UpdateServiceDeskHour(serviceDeskHour);
        }

        public async Task DeleteServiceDeskHour(string serviceDeskHourId)
    {
            
        }
        public async Task<string> GetNextId()
        {
            ServiceDeskHour lastServiceDeskHour = await _repository.GetLastServiceDeskHour();
            int id = 0;
            if (lastServiceDeskHour == null)
            {
                id = 1;
            }
            else
            {
                id = IDGenerator.ExtractNumberFromId(lastServiceDeskHour.ServiceDeskHourId) + 1;
            }
            string seriveId = IDGenerator.GenerateServiceDeskHourd(id);
            return seriveId;
        }
    }
}
