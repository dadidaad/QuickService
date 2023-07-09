using AutoMapper;
using QuickServiceWebAPI.DTOs.ServiceDeskHour;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class ServiceDeskHourService : IServiceDeskHourService
    {
        private readonly IServiceDeskHourRepository _repository;
        private readonly IMapper _mapper;
        public ServiceDeskHourService(IServiceDeskHourRepository repository, IMapper mapper)
        {
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
            if (!String.IsNullOrEmpty(createUpdateServiceDeskHourDTO.DayOfWeek))
            {
                serviceDeskHour.DayOfWeek = createUpdateServiceDeskHourDTO.DayOfWeek;
            }
            if (!String.IsNullOrEmpty(createUpdateServiceDeskHourDTO.BusinessHourId))
            {
                serviceDeskHour.BusinessHourId = createUpdateServiceDeskHourDTO.BusinessHourId;
            }
            serviceDeskHour = _mapper.Map<CreateUpdateServiceDeskHourDTO, ServiceDeskHour>(createUpdateServiceDeskHourDTO, serviceDeskHour);
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
