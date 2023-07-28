using AutoMapper;
using QuickServiceWebAPI.DTOs.YearHolidayList;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;

namespace QuickServiceWebAPI.Services.Implements
{
    public class YearlyHolidayListService : IYearlyHolidayListService
    {
        private readonly IYearlyHolidayListRepository _repository;
        private readonly IMapper _mapper;
        public YearlyHolidayListService(IYearlyHolidayListRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<YearlyHolidayListDTO> GetYearlyHolidayList()
        {
            var yearlyHolidayList = _repository.GetYearLyHolidayList();
            return yearlyHolidayList.Select(yearlyHoliday => _mapper.Map<YearlyHolidayListDTO>(yearlyHoliday)).ToList();
        }

        public async Task CreateYearlyHoliday(CreateUpdateYearlyHolidayListDTO createUpdateYearlyHolidayListDTO)
        {
            var yearlyHoliday = _mapper.Map<YearlyHolidayList>(createUpdateYearlyHolidayListDTO);
            await _repository.AddYearlyHoliday(yearlyHoliday);
        }

    }
}
