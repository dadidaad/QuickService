﻿using AutoMapper;
using QuickServiceWebAPI.DTOs.RequestTicket;
using QuickServiceWebAPI.DTOs.WorkflowAssignment;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class WorkflowAssignmentService : IWorkflowAssignmentService
    {
        private readonly IWorkflowAssignmentRepository _repository;
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IWorkflowTaskRepository _workflowTaskRepository;
        private readonly IRequestTicketRepository _requestTicketRepository;
        private readonly IWorkflowTransitionRepository _workflowTransitionRepository;
        private readonly IAttachmentService _attachmentService;
        private readonly ISlaRepository _slaRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public WorkflowAssignmentService(IWorkflowAssignmentRepository repository
            , IWorkflowRepository workflowRepository
            , IRequestTicketRepository requestTicketRepository
            , IWorkflowTaskRepository WorkflowTaskRepository
            , IAttachmentService attachmentService
            , IMapper mapper
            , IWorkflowTransitionRepository workflowTransitionRepository
            , ISlaRepository slaRepository
            , IUserRepository userRepository)
        {
            _repository = repository;
            _workflowRepository = workflowRepository;
            _requestTicketRepository = requestTicketRepository;
            _workflowTaskRepository = WorkflowTaskRepository;
            _attachmentService = attachmentService;
            _mapper = mapper;
            _workflowTransitionRepository = workflowTransitionRepository;
            _slaRepository = slaRepository;
            _userRepository = userRepository;
        }

        private static readonly Dictionary<StatusEnum, StatusWorkflowTaskEnum> StatusMapping = new Dictionary<StatusEnum, StatusWorkflowTaskEnum>
        {
            { StatusEnum.Open, StatusWorkflowTaskEnum.Open },
            { StatusEnum.InProgress, StatusWorkflowTaskEnum.InProgress},
            { StatusEnum.Pending, StatusWorkflowTaskEnum.Pending },
            { StatusEnum.Resolved, StatusWorkflowTaskEnum.Resolved}
        };

        public async Task AssignWorkflow(RequestTicket requestTicket, string? currentTaskId)
        {
            var workflowAssignment = new WorkflowAssignment
            {
                ReferenceId = requestTicket.RequestTicketId
            };
            var workflow = await _workflowRepository.GetWorkflowById(requestTicket.WorkflowId);
            var sla = await _slaRepository.GetSlaForRequestTicket(requestTicket);
            workflowAssignment.DueDate = DateTime.Now
                + TimeSpan.FromTicks(sla.Slametrics.FirstOrDefault(sm => sm.Priority == requestTicket.Priority).ResolutionTime);
            WorkflowTask workflowTask = null;
            if (currentTaskId == null)
            {
                workflowTask = await _workflowTaskRepository.GetOpenTaskOfWorkflow(workflow.WorkflowId);
            }
            else
            {
                workflowTask = await _workflowTaskRepository.GetWorkflowTaskById(currentTaskId);
                if(workflowTask == null)
                {
                    throw new AppException($"Workflow task with id {currentTaskId} not found");
                }
            }
            workflowAssignment.CurrentTaskId = workflowTask.WorkflowTaskId;
            workflowAssignment.WorkflowAssignmentId = await GetNextId();
            await _repository.AddWorkflowAssignment(workflowAssignment);
            
        }

        public async Task CompleteWorkflowTask(CheckWorkflowAssignmentDTO checkWorkflowAssignmentDTO)
        {
            var workflowAssignment = await _repository
                .GetWorkflowAssignmentByWorkflowAssignmentId(checkWorkflowAssignmentDTO.WorkflowAssignmentId);
            if (workflowAssignment == null)
            {
                throw new AppException($"Workflow assignment with id {checkWorkflowAssignmentDTO.WorkflowAssignmentId} not found");
            }
            var requestTicket = await _requestTicketRepository.GetRequestTicketById(workflowAssignment.ReferenceId);

            var currentWorkflowTask = await _workflowTaskRepository.GetWorkflowTaskById(workflowAssignment.CurrentTaskId);

            if (currentWorkflowTask.AssignerId == null)
            {
                throw new AppException($"Workflow task not yet assign to any agent!");
            }
            var user = await _userRepository.GetUserDetails(checkWorkflowAssignmentDTO.FinisherId);
            if(user == null)
            {
                throw new AppException($"User with id {checkWorkflowAssignmentDTO.FinisherId} not found");
            }
            if (checkWorkflowAssignmentDTO.FinisherId != currentWorkflowTask.AssignerId 
                && (currentWorkflowTask.Status != StatusWorkflowTaskEnum.Open.ToString() 
                || currentWorkflowTask.Status != StatusWorkflowTaskEnum.Resolved.ToString()))
            {
                throw new AppException($"Workflow task not assign to user have id {checkWorkflowAssignmentDTO.FinisherId}!");
            }
            var workflowTransitionForCurrentTask = await _workflowTransitionRepository.GetWorkflowTransitionById(workflowAssignment.CurrentTaskId, checkWorkflowAssignmentDTO.ToWorkFlowTask);
            if(workflowTransitionForCurrentTask == null)
            {
                throw new AppException($"Workflow transition for next task not found");
            }
            if (checkWorkflowAssignmentDTO.IsCompleted)
            {
                if (checkWorkflowAssignmentDTO.File != null)
                {
                    workflowAssignment.AttachmentId = (await _attachmentService.CreateAttachment(checkWorkflowAssignmentDTO.File)).AttachmentId;
                }
                var updateWorkflowAssignment = _mapper.Map(checkWorkflowAssignmentDTO, workflowAssignment);
                updateWorkflowAssignment.CompletedTime = DateTime.Now;
                await _repository.UpdateWorkflowAssignment(updateWorkflowAssignment);
                var nextWorkflowTask = await _workflowTaskRepository.GetWorkflowTaskById(checkWorkflowAssignmentDTO.ToWorkFlowTask);
                await HandleRequestTicketForCurrentTask(requestTicket, nextWorkflowTask);
                await AssignWorkflow(requestTicket, nextWorkflowTask.WorkflowTaskId);
            }
        }

        private async Task HandleRequestTicketForCurrentTask(RequestTicket requestTicket, WorkflowTask currentWorkTask)
        {
            requestTicket.Status = MappingWorkflowTaskStatusToRequestTicketStatus(currentWorkTask.Status.ToEnum(StatusWorkflowTaskEnum.Open)).ToString();
            requestTicket.AssignedTo = currentWorkTask.AssignerId;
            await _requestTicketRepository.UpdateRequestTicket(requestTicket);
        }

        private StatusEnum MappingWorkflowTaskStatusToRequestTicketStatus(StatusWorkflowTaskEnum statusWorkflowTaskEnum)
        {
            return StatusMapping.Where(s => s.Value == statusWorkflowTaskEnum).FirstOrDefault().Key;
        }

        public async Task DeleteListWorkflowAssignment(List<WorkflowAssignment> workflowAssignments)
        {
            foreach (var workflowAssignment in workflowAssignments)
            {
                if (!string.IsNullOrEmpty(workflowAssignment.AttachmentId))
                {
                    await _attachmentService.DeleteAttachment(workflowAssignment.AttachmentId);
                }
            }
            await _repository.DeleteRangeWorkflowAssignment(workflowAssignments);
        }

        public async Task<List<WorkflowAssignmentDTO>> GetWorkflowAssignmentsForTicket(string requestTicketId)
        {
            var requestTicket = await _requestTicketRepository.GetRequestTicketById(requestTicketId);
            if (requestTicket == null)
            {
                throw new AppException($"Request ticket with id {requestTicketId} not found");
            }
            var workflowAssignments = await _repository.GetWorkflowAssignmentsByRequestTicket(requestTicketId);
            return workflowAssignments.Select(wa => _mapper.Map<WorkflowAssignmentDTO>(wa)).ToList();
        }

        private async Task<string> GetNextId()
        {
            WorkflowAssignment lastWorkflowAssignment = await _repository.GetLastWorkflowAssignment();
            int id;
            if (lastWorkflowAssignment == null)
            {
                id = 1;
            }
            else
            {
                id = IDGenerator.ExtractNumberFromId(lastWorkflowAssignment.WorkflowAssignmentId) + 1;
            }
            string workflowAssignmentId = IDGenerator.GenerateWorkflowAssignmentId(id);
            return workflowAssignmentId;
        }

        public async Task<bool> CheckRequestTicketExists(string requestTicketId)
        {
            return await _repository.CheckExistingRequestTicket(requestTicketId);
        }

        public bool CheckStatusRequestTicketInStatusMapping(StatusEnum statusEnum)
        {
            return StatusMapping.ContainsKey(statusEnum);
        }

    }
}
