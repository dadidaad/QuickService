using System;
using System.Collections.Generic;

namespace QuickServiceWebAPI.Models;

public partial class Group
{
    public string GroupId { get; set; } = null!;

    public string GroupName { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsRestricted { get; set; }

    public bool NeedApprovalByLeader { get; set; }

    public string GroupLeader { get; set; } = null!;

    public string? BusinessHourId { get; set; }

    public virtual BusinessHour? BusinessHour { get; set; }

    public virtual ICollection<Change> Changes { get; set; } = new List<Change>();

    public virtual User GroupLeaderNavigation { get; set; } = null!;

    public virtual ICollection<Problem> Problems { get; set; } = new List<Problem>();

    public virtual ICollection<RequestTicket> RequestTickets { get; set; } = new List<RequestTicket>();

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();

    public virtual ICollection<WorkflowTask> WorkflowTasks { get; set; } = new List<WorkflowTask>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
