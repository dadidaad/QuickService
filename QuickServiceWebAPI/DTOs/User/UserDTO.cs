namespace QuickServiceWebAPI.DTOs.User
{
    public class UserDTO
    {
        public string? Email { get; set; } = null!;

        public string? Avatar { get; set; }

        public string? FullName { get; set; }

        public DateTime? BirthDate { get; set; }

        public string? JobTitle { get; set; }

        public string? Department { get; set; }

        public string? PersonalEmail { get; set; }

        public string? WallPaper { get; set; }
    }
}
