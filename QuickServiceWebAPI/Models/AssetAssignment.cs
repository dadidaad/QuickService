namespace QuickServiceWebAPI.Models;

public partial class AssetAssignment
{
    public string AssetAssignmentId { get; set; } = null!;

    public DateTime AssignedDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public string AssetId { get; set; } = null!;

    public string AssignedTo { get; set; } = null!;

    public virtual Asset Asset { get; set; } = null!;

    public virtual User AssignedToNavigation { get; set; } = null!;
}
