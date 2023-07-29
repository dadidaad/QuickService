using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.YearHolidayList
{
    public class CreateUpdateYearlyHolidayListDTO
    {
        [Required]
        public DateTime Holiday { get; set; }

        [Required]
        [MaxLength(100)]
        public string HolidayName { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string BusinessHourId { get; set; } = null!;
    }
}
