using AutoMapper;
using QuickServiceWebAPI.DTOs.ServiceType;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class ServiceTypeService : IServiceTypeService
    {
        private readonly IServiceTypeRepository _repository;
        private readonly IMapper _mapper;
        public ServiceTypeService(IServiceTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task CreateServiceType(CreateUpdateServiceTypeDTO createUpdateDTO)
        {
            var serviceType = _mapper.Map<ServiceType>(createUpdateDTO);
            serviceType.ServiceTypeId = await GetNextId();
            await _repository.AddServiceType(serviceType);
        }

        public List<ServiceTypeDTO> GetServiceTypes()
        {
            var serviceTypes = _repository.GetServiceTypes();
            return serviceTypes.Select(serviceType => _mapper.Map<ServiceTypeDTO>(serviceType)).ToList();
        }

        public async Task<ServiceTypeDTO> GetServiceTypeById(string serviceTypeId)
        {
            var serviceType = await _repository.GetServiceTypeById(serviceTypeId);
            return _mapper.Map<ServiceTypeDTO>(serviceType);
        }

        public async Task UpdateServiceType(string serviceTypeId, CreateUpdateServiceTypeDTO createUpdateDTO)
        {
            ServiceType serviceType = await _repository.GetServiceTypeById(serviceTypeId);
            if (serviceType == null)
            {
                throw new AppException("ServiceType not found");
            }
            serviceType = _mapper.Map<CreateUpdateServiceTypeDTO, ServiceType>(createUpdateDTO, serviceType);
            await _repository.UpdateServiceType(serviceType);
        }

        public async Task DeleteServiceType(string serviceTypeId)
        {
            var serviceType = await _repository.GetServiceTypeById(serviceTypeId);
            await _repository.DeleteServiceType(serviceType);
        }

        public async Task<string> GetNextId()
        {
            ServiceType lastServiceType = await _repository.GetLastServiceType();
            int id = 0;
            if (lastServiceType == null)
            {
                id = 1;
            }
            else
            {
                id = IDGenerator.ExtractNumberFromId(lastServiceType.ServiceTypeId) + 1;
            }
            string seriveTypeId = IDGenerator.GenerateServiceTypeId(id);
            return seriveTypeId;
        }

    }
}
