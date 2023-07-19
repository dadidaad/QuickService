using System;
using System.Collections.Generic;

namespace QuickServiceWebAPI.Models;

public partial class Attachment
{
    public string AttachmentId { get; set; } = null!;

    public string ReferenceId { get; set; } = null!;

    public string ReferenceType { get; set; } = null!;

    public string Filename { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public int FileSize { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<RequestTicket> RequestTickets { get; set; } = new List<RequestTicket>();

}
