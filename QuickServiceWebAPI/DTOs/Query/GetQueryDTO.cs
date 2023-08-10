using QuickServiceWebAPI.DTOs.User;

namespace QuickServiceWebAPI.DTOs.Query
{
    public class GetQueryDTO
    {
        public string QueryId { get; set; } = null!;

        public string? QueryName { get; set; }

        public string? QueryStatement { get; set; }

        public bool? IsTeamQuery { get; set; }

        public string? UserId { get; set; }

        public virtual UserDTO? UserEntity { get; set; }
    }
}
