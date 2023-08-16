using QuickServiceWebAPI.DTOs.BusinessHour;
using QuickServiceWebAPI.DTOs.User;

namespace QuickServiceWebAPI.DTOs.Group
{
    public class GroupDTO
    {
        public string GroupId { get; set; } = null!;

        public string GroupName { get; set; } = null!;

        public string? Description { get; set; }

        public bool IsRestricted { get; set; }

        public bool NeedApprovalByLeader { get; set; }

        public string GroupLeader { get; set; } = null!;

        public virtual UserDTO UserEntity { get; set; } = null!;
    }
}
