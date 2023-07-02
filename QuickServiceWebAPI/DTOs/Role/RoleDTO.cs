using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.DTOs.Role
{
    public class RoleDTO
    {
        public string RoleId { get; set; } = null!;

        public string RoleName { get; set; } = null!;

        public RoleType RoleType { get; set; }

        public string? Description { get; set; }
    }
}
