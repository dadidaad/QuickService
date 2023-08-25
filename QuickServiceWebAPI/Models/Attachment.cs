namespace QuickServiceWebAPI.Models;

public partial class Attachment
{
    public string AttachmentId { get; set; } = null!;

    public string Filename { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public int FileSize { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Change> Changes { get; set; } = new List<Change>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Problem> Problems { get; set; } = new List<Problem>();

    public virtual ICollection<RequestTicket> RequestTickets { get; set; } = new List<RequestTicket>();

    public virtual ICollection<WorkflowAssignment> WorkflowAssignments { get; set; } = new List<WorkflowAssignment>();
}
