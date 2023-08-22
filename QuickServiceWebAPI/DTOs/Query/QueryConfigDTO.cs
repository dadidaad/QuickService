namespace QuickServiceWebAPI.DTOs.Query
{
    public class QueryConfigDTO
    {
        public string? OrderyBy { get; set; }
        public bool? OrderASC { get; set; }
        public bool? IsIncident { get; set; }
        public string[]? Priority { get; set; }
        public string[]? Status { get; set; }        
        public string[]? RequestType { get; set; }                           
        public string[]? Service { get; set; }
        public string[]? Assignee { get; set; }
        public string[]? Reporter { get; set; }
        public string[]? Group { get; set; }
        public string? TitleSearch { get; set; }
        public DateTime? CreatedTo { get; set; }
        public DateTime? CreatedFrom { get; set; }     
    }
}
