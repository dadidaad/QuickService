namespace QuickServiceWebAPI.DTOs.Query
{
    public class QueryDTO
    {
        public string? Assignee { get; set; }
        public DateTime? CreateFrom { get; set; }
        public DateTime? CreateTo { get; set; }           
        public string? Description { get; set; }      
        public string? Group { get; set; }
        public string[]? Requester { get; set; }
        public string? RequestType { get; set; }
        public string[]? Priority { get; set; }
        public string[]? Status { get; set; }
        
    }
}
