﻿using QuickServiceWebAPI.Models.Enums;
using System;
using System.Collections.Generic;

namespace QuickServiceWebAPI.Models;

public partial class Role
{
    public string RoleId { get; set; } = null!;

    public string RoleName { get; set; } = null!;

    public RoleType RoleType { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}

