namespace QuickServiceWebAPI.DTOs.Permission
{
    public class PermissionForRoleResponseDTO
    {
        public string? RoleId { get; set; }

        public Dictionary<string, bool>? Permissions { get; set; }
    }
}
