using AutoMapper;
using QuickServiceWebAPI.DTOs.WorkflowTask;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Repositories.Implements;
using QuickServiceWebAPI.Utilities;
using System.Runtime.InteropServices;

namespace QuickServiceWebAPI.Services.Implements
{
    public class WorkflowTaskService : IWorkflowTaskService
    {
        private readonly IWorkflowTaskRepository _repository;
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IWorkflowAssignmentRepository _workflowAssignmentRepository;
        private readonly IWorkflowAssignmentService _workflowAssignmentService;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IRequestTicketRepository _requestTicketRepository;

        public WorkflowTaskService(IWorkflowTaskRepository repository, IWorkflowRepository workflowRepository,
            IWorkflowAssignmentRepository workflowAssignmentRepository, IWorkflowAssignmentService workflowAssignmentService,
            IMapper mapper,
            IUserRepository userRepository, IGroupRepository groupRepository,
            IRequestTicketRepository requestTicketRepository)
        {
            _repository = repository;
            _workflowRepository = workflowRepository;
            _workflowAssignmentRepository = workflowAssignmentRepository;
            _workflowAssignmentService = workflowAssignmentService;
            _mapper = mapper;
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _requestTicketRepository = requestTicketRepository;
        }

        public async Task<List<WorkflowTaskDTO>> GetWorkflowsTaskByWorkflow(string workflowId)
        {
            var workflow = await _workflowRepository.GetWorkflowById(workflowId);
            if (workflow == null)
            {
                throw new AppException($"Workflow with id {workflowId} not found");
            }
            var workflowTasks = await _repository.GetWorkflowTaskByWorkflow(workflowId);
            return workflowTasks.Select(workflowTask => _mapper.Map<WorkflowTaskDTO>(workflowTask)).ToList();
        }

        public async Task<WorkflowTaskDTO> GetWorkflowTaskById(string workflowTaskId)
        {
            var workflowTask = await _repository.GetWorkflowTaskById(workflowTaskId);
            return _mapper.Map<WorkflowTaskDTO>(workflowTask);
        }

        public async Task<WorkflowTaskDTO?> CreateWorkflowTask(CreateUpdateWorkflowTaskDTO createUpdateWorkflowTaskDTO, bool AcceptResovledAndOpenTask)
        {
            var workflow = await _workflowRepository.GetWorkflowById(createUpdateWorkflowTaskDTO.WorkflowId);
            if (workflow == null)
            {
                throw new AppException($"Workflow with id {createUpdateWorkflowTaskDTO.WorkflowId} not found");
            }
            await HandleEditWorkflowTask(workflow.WorkflowId);
            if ((createUpdateWorkflowTaskDTO.Status == StatusWorkflowTaskEnum.Resolved.ToString()
                || createUpdateWorkflowTaskDTO.Status == StatusWorkflowTaskEnum.Open.ToString())
                && !AcceptResovledAndOpenTask)
            {
                throw new AppException($"Workflow already have resolved and open Task");
            }
            await ValidationUserGroup(createUpdateWorkflowTaskDTO);
            var workflowTask = _mapper.Map<WorkflowTask>(createUpdateWorkflowTaskDTO);
            workflowTask.WorkflowTaskId = await GetNextId();
            workflowTask.CreatedDate = DateTime.Now;
            var workflowTaskAdded = await _repository.AddWorkflowTask(workflowTask);
            return workflowTaskAdded != null ? _mapper.Map<WorkflowTaskDTO>(workflowTaskAdded) : null;
        }

        public async Task UpdateWorkflowTask(string workflowTaskId, CreateUpdateWorkflowTaskDTO createUpdateWorkflowTaskDTO)
        {
            WorkflowTask workflowTask = await _repository.GetWorkflowTaskById(workflowTaskId);
            if (workflowTask == null)
            {
                throw new AppException("WorkflowTask not found");
            }
            await HandleEditWorkflowTask(workflowTask.WorkflowId);

            if (await _workflowRepository.GetWorkflowById(createUpdateWorkflowTaskDTO.WorkflowId) == null)
            {
                throw new AppException("Workflow with id " + createUpdateWorkflowTaskDTO.WorkflowId + " not found");
            }
            if (workflowTask.Status == StatusWorkflowTaskEnum.Open.ToString() || workflowTask.Status == StatusWorkflowTaskEnum.Resolved.ToString())
            {
                if (createUpdateWorkflowTaskDTO.Status != workflowTask.Status)
                {
                    throw new AppException($"Can not update link status for open and resoveled workflow task");
                }
            }
            await ValidationUserGroup(createUpdateWorkflowTaskDTO);
            workflowTask = _mapper.Map(createUpdateWorkflowTaskDTO, workflowTask);
            await _repository.UpdateWorkflowTask(workflowTask);
        }

        private async Task ValidationUserGroup(CreateUpdateWorkflowTaskDTO createUpdateWorkflowTaskDTO)
        {
            if (!string.IsNullOrEmpty(createUpdateWorkflowTaskDTO.AssignerId) && string.IsNullOrEmpty(createUpdateWorkflowTaskDTO.GroupId))
            {
                var user = await _userRepository.GetUserDetails(createUpdateWorkflowTaskDTO.AssignerId);
                if (user == null)
                {
                    throw new AppException($"User with id {createUpdateWorkflowTaskDTO.AssignerId} not found");
                }
            }
            else if (!string.IsNullOrEmpty(createUpdateWorkflowTaskDTO.GroupId) && string.IsNullOrEmpty(createUpdateWorkflowTaskDTO.AssignerId))
            {
                var group = await _groupRepository.GetGroupById(createUpdateWorkflowTaskDTO.GroupId);
                if (group == null)
                {
                    throw new AppException($"Group with id {createUpdateWorkflowTaskDTO.GroupId} not found");
                }
            }
            else if (string.IsNullOrEmpty(createUpdateWorkflowTaskDTO.GroupId) && string.IsNullOrEmpty(createUpdateWorkflowTaskDTO.AssignerId))
            {

            }
            else
            {
                throw new AppException($"Only accept group or assignee");
            }

        }

        public async Task DeleteWorkflowTask(string workflowTaskId)
        {
            var workflowTask = await _repository.GetWorkflowTaskById(workflowTaskId);
            if (workflowTask == null)
            {
                throw new AppException($"Workflow Task with id {workflowTaskId} not found");
            }
            await HandleEditWorkflowTask(workflowTask.WorkflowId);
            var listWorkflowAssignment = await _workflowAssignmentRepository.GetWorkflowAssignmentsByWorkflowTaskId(workflowTaskId);
            if (listWorkflowAssignment.IsAny())
            {
                await _workflowAssignmentService.DeleteListWorkflowAssignment(listWorkflowAssignment);
            }
            await _repository.DeleteWorkflowTask(workflowTask);
        }

        private async Task<bool> CheckStatusRequestTicketToEditWorkflowTask(string workflowId)
        {
            var workflow = await _workflowRepository.GetWorkflowById(workflowId);
            if (workflow == null)
            {
                throw new AppException($"Workflow with id {workflowId} not found");
            }
            var requestTickets = await _requestTicketRepository.GetAllRequestTicketRelatedToWorkflow(workflowId);
            if (requestTickets.Any(r => r.Status != StatusEnum.Resolved.ToString()
            || r.Status != StatusEnum.Canceled.ToString()
            || r.Status != StatusEnum.Closed.ToString()))
            {
                return false;
            }
            return true;
        }

        private async Task HandleEditWorkflowTask(string workflowId)
        {
            bool checkConditionEdit = await CheckStatusRequestTicketToEditWorkflowTask(workflowId);
            if (!checkConditionEdit)
            {
                throw new AppException("Can not edit workflow already assign task to request ticket");
            }
        }

        public async Task<string> GetNextId()
        {
            WorkflowTask lastWorkflowTask = await _repository.GetLastWorkflowTask();
            int id;
            if (lastWorkflowTask == null)
            {
                id = 1;
            }
            else
            {
                id = IDGenerator.ExtractNumberFromId(lastWorkflowTask.WorkflowTaskId) + 1;
            }
            string workflowTaskId = IDGenerator.GenerateWorkflowTaskId(id);
            return workflowTaskId;
        }
    }
}
