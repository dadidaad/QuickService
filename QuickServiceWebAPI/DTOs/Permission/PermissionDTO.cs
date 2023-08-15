namespace QuickServiceWebAPI.DTOs.Permission
{
    public class PermissionDTO
    {
        public string PermissionId { get; set; } = null!;

        public string PermissionName { get; set; } = null!;

        public bool IsGranted { get; set; }
    }
}
