using System;
using System.Collections.Generic;

namespace QuickServiceWebAPI.Models;

public partial class Permission
{
    public string PermissionId { get; set; } = null!;

    public string PermissionName { get; set; } = null!;

    public string? PermissionModule { get; set; }
    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
