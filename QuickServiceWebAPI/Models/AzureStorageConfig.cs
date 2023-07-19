namespace QuickServiceWebAPI.Models
{
    public class AzureStorageConfig
    {
        public string? AccountName { get; set; }
        public string? AccountKey { get; set; }
        public string? ImageContainer { get; set; }
        public string? ThumbnailContainer { get; set; }
        public string? AttachmentContainer { get; set; }
        public string? IconServiceItemContainer { get; set; }
    }
}
