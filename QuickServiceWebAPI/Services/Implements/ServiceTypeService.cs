using AutoMapper;
using Microsoft.Extensions.Options;
using QuickServiceWebAPI.DTOs.ServiceType;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;
using System.Collections.Generic;

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

        public async Task UpdateServiceType(string serviceTypeId, CreateUpdateServiceTypeDTO createUpdateDTO)
        {
            ServiceType serviceType = await _repository.GetServiceTypeById(serviceTypeId);
            if (serviceType == null)
            {
                throw new AppException("ServiceType not found");
            }
            if (!String.IsNullOrEmpty(createUpdateDTO.ServiceTypeName))
            {
                serviceType.ServiceTypeName = createUpdateDTO.ServiceTypeName;
            }
            if (!String.IsNullOrEmpty(createUpdateDTO.Description))
            {
                serviceType.Description = createUpdateDTO.Description;
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
