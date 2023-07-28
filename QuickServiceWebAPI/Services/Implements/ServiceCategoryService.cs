using AutoMapper;
using QuickServiceWebAPI.DTOs.ServiceCategory;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class ServiceCategoryService : IServiceCategoryService
    {
        private readonly IServiceCategoryRepository _repository;
        private readonly IMapper _mapper;
        public ServiceCategoryService(IServiceCategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<ServiceCategoryDTO> GetServiceCategories()
        {
            var serviceCategories = _repository.GetServiceCategories();
            return serviceCategories.Select(serviceCategory => _mapper.Map<ServiceCategoryDTO>(serviceCategory)).ToList();
        }

        public async Task<ServiceCategoryDTO> GetServiceCategoryById(string serviceCategoryId)
        {
            var serviceCategory = await _repository.GetServiceCategoryById(serviceCategoryId);
            return _mapper.Map<ServiceCategoryDTO>(serviceCategory);
        }

        public async Task CreateServiceCategory(CreateUpdateServiceCategoryDTO createUpdateServiceCategoryDTO)
        {
            var serviceCategory = _mapper.Map<ServiceCategory>(createUpdateServiceCategoryDTO);
            serviceCategory.ServiceCategoryId = await GetNextId();
            await _repository.AddServiceCategory(serviceCategory);
        }

        public async Task UpdateServiceCategory(string serviceCategoryId, CreateUpdateServiceCategoryDTO createUpdateServiceCategoryDTO)
        {
            ServiceCategory serviceCategory = await _repository.GetServiceCategoryById(serviceCategoryId);
            if (serviceCategory == null)
            {
                throw new AppException("ServiceCategory not found");
            }
            serviceCategory = _mapper.Map(createUpdateServiceCategoryDTO, serviceCategory);
            await _repository.UpdateServiceCategory(serviceCategory);
        }

        public async Task DeleteServiceCategory(string serviceCategoryId)
        {
            var serviceCategory = await _repository.GetServiceCategoryById(serviceCategoryId);
            await _repository.DeleteServiceCategory(serviceCategory);
        }
        public async Task<string> GetNextId()
        {
            ServiceCategory lastServiceCategory = await _repository.GetLastServiceCategory();
            int id = 0;
            if (lastServiceCategory == null)
            {
                id = 1;
            }
            else
            {
                id = IDGenerator.ExtractNumberFromId(lastServiceCategory.ServiceCategoryId) + 1;
            }
            string seriveId = IDGenerator.GenerateServiceCategoryId(id);
            return seriveId;
        }
    }
}
