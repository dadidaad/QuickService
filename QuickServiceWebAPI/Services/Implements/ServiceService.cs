using AutoMapper;
using QuickServiceWebAPI.DTOs.Service;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IServiceTypeRepository _serviceTypeRepository;
        private readonly IMapper _mapper;
        public ServiceService(IServiceRepository repository, IUserRepository userRepository, IGroupRepository groupRepository, IServiceTypeRepository serviceTypeRepository, IMapper mapper)
        {
            _repository = repository;
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _serviceTypeRepository = serviceTypeRepository;
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
            if (service == null)
            {
                throw new AppException("Service not found");
            }
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
            if (_userRepository.GetUserDetails(createUpdateServiceDTO.CreatedBy) == null)
            {
                throw new AppException("Create by user with id  " + createUpdateServiceDTO.CreatedBy + " not found");
            }
            if (_groupRepository.GetGroupById(createUpdateServiceDTO.ManagedByGroup) == null)
            {
                throw new AppException("Manage by group with id " + createUpdateServiceDTO.ManagedByGroup + " not found");
            }
            if (_userRepository.GetUserDetails(createUpdateServiceDTO.ManagedBy) == null)
            {
                throw new AppException("Manage by user with id  " + createUpdateServiceDTO.ManagedBy + " not found");
            }
            if (_serviceTypeRepository.GetServiceTypeById(createUpdateServiceDTO.ServiceTypeId) == null)
            {
                throw new AppException("Service type with id  " + createUpdateServiceDTO.ServiceTypeId + " not found");
            }
            service = _mapper.Map(createUpdateServiceDTO, service);
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
