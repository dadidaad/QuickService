namespace QuickServiceWebAPI.DTOs.Dashboard
{
    public class CountByDayDTO
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool NeedDividedByServiceItem { get; set; }  
    }
}
