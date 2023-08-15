namespace QuickServiceWebAPI.DTOs.Permission
{
    public class PermissionDTO
    {
        public string PermissionId { get; set; } = null!;

        public string? PermissionName { get; set; }

        public bool IsGranted { get; set; }
    }
}
