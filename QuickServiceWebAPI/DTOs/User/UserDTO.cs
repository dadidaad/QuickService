using QuickServiceWebAPI.DTOs.Role;
using System.Text.Json.Serialization;

namespace QuickServiceWebAPI.DTOs.User
{
    public class UserDTO
    {
        public string? UserId { get; set; } = null!;

        public string? Email { get; set; } = null!;

        public string? Password { get; set; } = null!;

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; } = null!;

        public DateTime? CreatedTime { get; set; }

        public string? RoleId { get; set; } = null!;

        public string? Avatar { get; set; }

        public bool? IsActive { get; set; }

        public virtual RoleDTO? Role { get; set; } = null!;
    }
}
