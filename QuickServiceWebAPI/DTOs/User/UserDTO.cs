namespace QuickServiceWebAPI.DTOs.User
{
    public class UserDTO
    {
        public string? Email { get; set; } = null!;

        public string? Avatar { get; set; }

        public string? PhoneNumber { get; set; }

        public string? FullName { get; set; }
    }
}
