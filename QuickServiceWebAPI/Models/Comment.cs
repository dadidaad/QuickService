namespace QuickServiceWebAPI.Models;

public partial class Comment
{
    public string CommentId { get; set; } = null!;

    public string? CommentText { get; set; }

    public DateTime CommentTime { get; set; }

    public bool IsInternal { get; set; }

    public string CommentBy { get; set; } = null!;

    public string RequestTicketId { get; set; } = null!;

    public string? AttachmentId { get; set; }

    public DateTime? LastModified { get; set; }

    public virtual Attachment? Attachment { get; set; }

    public virtual User CommentByNavigation { get; set; } = null!;

    public virtual RequestTicket RequestTicket { get; set; } = null!;
}
