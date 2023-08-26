namespace QuickServiceWebAPI.DTOs.RequestTicket
{
    public class TicketQueryAdminDTO
    {
        public string TicketId { get; set; } = null!;
        public bool IsIncident { get; set; } = false;
        public string Title { get; set; } = null!;
        public string? ServiceCategoryId { get; set; } = null!;

        public string? ServiceCategoryName { get; set; } = null!;

        public string? ServiceItemId { get; set; } = null!;

        public string? ServiceItemName { get; set; } = null!;

        public string GroupId { get; set; } = null!;

        public string GroupName { get; set; } = null!;
        public string RequesterId { get; set; } = null!;

        public string? RequesterFullName { get; set; }
        public string? RequesterAvatar { get; set; }

        public string? AssigneeId { get; set; } = null!;

        public string? AssigneeFullName { get; set; }
        public string? AssigneeAvatar { get; set; }

        public string Status { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
        public string Priority { get; set; } = null!;
        public string Type { get; set; } = null!;

    }
}
