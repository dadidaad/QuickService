namespace QuickServiceWebAPI.DTOs.BusinessHour
{
    public class BusinessHourDTO
    {
        public string BusinessHourId { get; set; } 

        public string BusinessHourName { get; set; } 

        public string TimeZone { get; set; } 

        public bool IsDefault { get; set; }
    }
}
