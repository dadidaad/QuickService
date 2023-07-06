using AutoMapper;
using QuickServiceWebAPI.DTOs.Service;
using QuickServiceWebAPI.DTOs.WorkflowStep;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _repository;
        private readonly IMapper _mapper;
        public ServiceService(IServiceRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<ServiceDTO> GetServices()
        {
            var services = _repository.GetServices();
            return services.Select(service => _mapper.Map<ServiceDTO>(service)).ToList();
        }

        public async Task<ServiceDTO> GetServiceById(string serviceId)
        {
            var service = await _repository.GetServiceById(serviceId);
            return _mapper.Map<ServiceDTO>(service);
        }

        public async Task CreateService(CreateUpdateServiceDTO createUpdateServiceDTO)
        {
            var service = _mapper.Map<Service>(createUpdateServiceDTO);
            service.ServiceId = await GetNextId();
            service.CreatedAt = DateTime.Now;
            await _repository.AddService(service);
        }

        public async Task UpdateService(string serviceId, CreateUpdateServiceDTO createUpdateServiceDTO)
        {
            Service service = await _repository.GetServiceById(serviceId);
            if (service == null)
            {
                throw new AppException("Service not found");
            }
            if (!String.IsNullOrEmpty(createUpdateServiceDTO.ServiceName))
            {
                service.ServiceName = createUpdateServiceDTO.ServiceName;
            }
            if (!String.IsNullOrEmpty(createUpdateServiceDTO.Description))
            {
                service.Description = createUpdateServiceDTO.Description;
            }
            if (!String.IsNullOrEmpty(createUpdateServiceDTO.Impact))
            {
                service.Impact = createUpdateServiceDTO.Impact;
            }
            if (!String.IsNullOrEmpty(createUpdateServiceDTO.HealthStatus))
            {
                service.HealthStatus = createUpdateServiceDTO.HealthStatus;
            }
            if (!String.IsNullOrEmpty(createUpdateServiceDTO.CreatedBy))
            {
                service.CreatedBy = createUpdateServiceDTO.CreatedBy;
            }
            if (!String.IsNullOrEmpty(createUpdateServiceDTO.ServiceTypeId))
            {
                service.ServiceTypeId = createUpdateServiceDTO.ServiceTypeId;
            }
            if (!String.IsNullOrEmpty(createUpdateServiceDTO.ManagedBy))
            {
                service.ManagedBy = createUpdateServiceDTO.ManagedBy;
            }
            if (!String.IsNullOrEmpty(createUpdateServiceDTO.ManagedByGroup))
            {
                service.ManagedByGroup = createUpdateServiceDTO.ManagedByGroup;
            }
            service = _mapper.Map<CreateUpdateServiceDTO, Service>(createUpdateServiceDTO, service);
            await _repository.UpdateService(service);
        }

        public async Task DeleteService(string serviceId)
        {
            var service = await _repository.GetServiceById(serviceId);
            await _repository.DeleteService(service);
        }
        public async Task<string> GetNextId()
        {
            Service lastService = await _repository.GetLastService();
            int id = 0;
            if (lastService == null)
            {
                id = 1;
            }
            else
            {
                id = IDGenerator.ExtractNumberFromId(lastService.ServiceId) + 1;
            }
            string seriveId = IDGenerator.GenerateServiceId(id);
            return seriveId;
        }      
    }
}
