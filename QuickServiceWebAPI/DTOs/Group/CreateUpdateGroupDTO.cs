namespace QuickServiceWebAPI.DTOs.Group
{
    public class CreateUpdateGroupDTO
    {
        public string GroupName { get; set; } = null!;

        public string? Description { get; set; }

        public bool IsRestricted { get; set; }

        public bool NeedApprovalByLeader { get; set; }

        public string GroupLeader { get; set; } = null!;

        public string BusinessHourId { get; set; } = null!;
    }
}
