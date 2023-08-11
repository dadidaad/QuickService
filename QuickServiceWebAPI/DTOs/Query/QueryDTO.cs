namespace QuickServiceWebAPI.DTOs.Query
{
    public class QueryDTO
    {
        public string? OrderyBy { get; set; }
        public bool? OrderASC { get; set; }
        public string[]? Priority { get; set; }
        public string[]? Status { get; set; }        
        public string[]? RequestType { get; set; }                           
        public string[]? Service { get; set; }
        public string[]? Assignee { get; set; }
        public string[]? Reporter { get; set; }
        public string[]? Group { get; set; }
        public string? Description { get; set; }
        public DateTime? CreateTo { get; set; }
        public DateTime? CreateFrom { get; set; }     
    }
}
