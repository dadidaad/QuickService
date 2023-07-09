using QuickServiceWebAPI.CustomAttributes;
using System.ComponentModel.DataAnnotations;

namespace QuickServiceWebAPI.DTOs.Permission
{
    public class UpdatePermissionsDTO
    {
        [Required]
        [MaxLength(10)]
        public string? RoleId { get; set; }

        [Required]
        [DictionaryValid]
        public Dictionary<string, bool>? Permissions { get; set; }
    }
}
