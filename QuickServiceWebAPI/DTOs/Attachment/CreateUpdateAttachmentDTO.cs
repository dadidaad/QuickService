namespace QuickServiceWebAPI.DTOs.Attachment
{
    public class CreateUpdateAttachmentDTO
    {
        public string ReferenceId { get; set; } = null!;

        public string ReferenceType { get; set; } = null!;

        public string Filename { get; set; } = null!;

        public string FilePath { get; set; } = null!;

        public int FileSize { get; set; }

    }
}
