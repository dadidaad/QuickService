namespace QuickServiceWebAPI.DTOs.WorkflowStep
{
    public class CreateUpdateWorkflowStepDTO
    {
        public string WorkflowStepName { get; set; } 

        public string ActionType { get; set; } 

        public string ActionDetails { get; set; } 

        public string WorkflowId { get; set; } 
    }
}
