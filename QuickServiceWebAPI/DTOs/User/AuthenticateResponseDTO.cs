namespace QuickServiceWebAPI.DTOs.User
{
    public class AuthenticateResponseDTO
    {
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
