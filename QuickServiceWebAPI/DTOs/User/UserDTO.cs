using QuickServiceWebAPI.DTOs.Role;
using System.Text.Json.Serialization;

namespace QuickServiceWebAPI.DTOs.User
{
    public class UserDTO
    {
        public string? UserId { get; set; } = null!;

        public string? Email { get; set; } = null!;

        public virtual RoleDTO? Role { get; set; } = null!;
    }
}
