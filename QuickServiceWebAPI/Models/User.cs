using System;
using System.Collections.Generic;

namespace QuickServiceWebAPI.Models;

public partial class User
{
    public string UserId { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime CreatedTime { get; set; }

    public string? RoleId { get; set; }

    public string? Avatar { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? JobTitle { get; set; }

    public string? Department { get; set; }

    public string? PersonalEmail { get; set; }

    public string? WallPaper { get; set; }

    public virtual ICollection<AssetAssignment> AssetAssignments { get; set; } = new List<AssetAssignment>();

    public virtual ICollection<Change> ChangeAssignees { get; set; } = new List<Change>();

    public virtual ICollection<Change> ChangeRequesters { get; set; } = new List<Change>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<Notification> NotificationFromUsers { get; set; } = new List<Notification>();

    public virtual ICollection<Notification> NotificationToUsers { get; set; } = new List<Notification>();

    public virtual ICollection<Problem> ProblemAssignees { get; set; } = new List<Problem>();

    public virtual ICollection<Problem> ProblemRequesters { get; set; } = new List<Problem>();

    public virtual ICollection<Query> Queries { get; set; } = new List<Query>();

    public virtual ICollection<RequestTicket> RequestTicketAssignedToNavigations { get; set; } = new List<RequestTicket>();

    public virtual ICollection<RequestTicketHistory> RequestTicketHistories { get; set; } = new List<RequestTicketHistory>();

    public virtual ICollection<RequestTicket> RequestTicketRequesters { get; set; } = new List<RequestTicket>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<Service> ServiceCreatedByNavigations { get; set; } = new List<Service>();

    public virtual ICollection<Service> ServiceManagedByNavigations { get; set; } = new List<Service>();

    public virtual ICollection<WorkflowAssignment> WorkflowAssignments { get; set; } = new List<WorkflowAssignment>();

    public virtual ICollection<WorkflowTask> WorkflowTasks { get; set; } = new List<WorkflowTask>();

    public virtual ICollection<Workflow> Workflows { get; set; } = new List<Workflow>();

    public virtual ICollection<Group> GroupsNavigation { get; set; } = new List<Group>();
}
