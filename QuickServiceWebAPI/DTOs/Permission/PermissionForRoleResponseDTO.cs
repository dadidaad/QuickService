namespace QuickServiceWebAPI.DTOs.Permission
{
    public class PermissionForRoleResponseDTO
    {
        public string? RoleId { get; set; }

        public List<PermissionDTO> Permissions { get; set; } = new List<PermissionDTO>();
    }
}
