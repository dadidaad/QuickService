using QuickServiceWebAPI.DTOs.ServiceItem;

namespace QuickServiceWebAPI.DTOs.Dashboard
{
    public class CountRequestTicketByDayDTO
    {
        public DateTime Date { get; set; }
        
        public int TotalCreated { get; set; }

        public int TotalResolved { get; set; }

        public string? ServiceItemName { get; set; }

    }
}
