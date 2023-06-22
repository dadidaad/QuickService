using System;
using System.Collections.Generic;

namespace QuickServiceWebAPI.Models;

public partial class WorkflowAssignment
{
    public string WorkflowAssignmentId { get; set; } = null!;

    public bool IsCompleted { get; set; }

    public string RequestTicketId { get; set; } = null!;

    public string WorkflowId { get; set; } = null!;

    public string CurrentStepId { get; set; } = null!;

    public virtual WorkflowStep CurrentStep { get; set; } = null!;

    public virtual RequestTicket RequestTicket { get; set; } = null!;

    public virtual Workflow Workflow { get; set; } = null!;
}
