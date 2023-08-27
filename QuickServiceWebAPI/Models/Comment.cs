using System;
using System.Collections.Generic;

namespace QuickServiceWebAPI.Models;

public partial class Comment
{
    public string CommentId { get; set; } = null!;

    public string? CommentText { get; set; }

    public DateTime CommentTime { get; set; }

    public bool IsInternal { get; set; }

    public string CommentBy { get; set; } = null!;

    public string? RequestTicketId { get; set; }

    public string? AttachmentId { get; set; }

    public DateTime? LastModified { get; set; }

    public string? ChangeId { get; set; }

    public string? ProblemId { get; set; }

    public virtual Attachment? Attachment { get; set; }

    public virtual Change? Change { get; set; }

    public virtual User CommentByNavigation { get; set; } = null!;

    public virtual Problem? Problem { get; set; }

    public virtual RequestTicket? RequestTicket { get; set; }
}
