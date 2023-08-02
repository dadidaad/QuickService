using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.Group
{
    public class CreateUpdateGroupDTO
    {
        [Required]
        [MaxLength(100)]
        public string GroupName { get; set; } = null!;

        [MaxLength(255)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(10)]
        public string GroupLeader { get; set; } = null!;

        [Required]
        [MaxLength(10)]
        public string BusinessHourId { get; set; } = null!;
    }
}
