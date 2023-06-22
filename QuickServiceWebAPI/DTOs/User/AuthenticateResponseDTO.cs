namespace QuickServiceWebAPI.DTOs.User
{
    public class AuthenticateResponseDTO
    {
        public string UserId { get; set; } 
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }   
        public string Token { get; set; }
    }
}
