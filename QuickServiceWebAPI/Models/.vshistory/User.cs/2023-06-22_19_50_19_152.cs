using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace QuickServiceWebAPI.Models;

public partial class User
{
    public string? UserId { get; set; } = null!;

    public string? Email { get; set; } = null!;

    [JsonIgnore]
    public string? Password { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string? PhoneNumber { get; set; } = null!;

    public DateTime? CreatedTime { get; set; }

    public string? RoleId { get; set; } = null!;

    public string? Avatar { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<AssetAssignment> AssetAssignments { get; set; } = new List<AssetAssignment>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<RequestTicket> RequestTicketAssignedToNavigations { get; set; } = new List<RequestTicket>();

    public virtual ICollection<RequestTicket> RequestTicketRequesters { get; set; } = new List<RequestTicket>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Service> ServiceCreatedByNavigations { get; set; } = new List<Service>();

    public virtual ICollection<Service> ServiceManagedByNavigations { get; set; } = new List<Service>();

    public virtual ICollection<Workflow> Workflows { get; set; } = new List<Workflow>();

    public virtual ICollection<Group> GroupsNavigation { get; set; } = new List<Group>();
}
