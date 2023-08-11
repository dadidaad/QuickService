using AutoMapper;
using QuickServiceWebAPI.DTOs.WorkflowTask;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

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
        public WorkflowTaskService(IWorkflowTaskRepository repository,
            IWorkflowRepository workflowRepository, IMapper mapper,
            IUserRepository userRepository, IGroupRepository groupRepository,
            IWorkflowAssignmentRepository workflowAssignmentRepository,
            IWorkflowAssignmentService workflowAssignmentService)
        {
            _repository = repository;
            _workflowRepository = workflowRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _workflowAssignmentRepository = workflowAssignmentRepository;
            _workflowAssignmentService = workflowAssignmentService;
        }

        public List<WorkflowTaskDTO> GetWorkflowsTask()
        {
            var workflowTasks = _repository.GetWorkflowTasks();
            return workflowTasks.Select(workflowTask => _mapper.Map<WorkflowTaskDTO>(workflowTask)).ToList();
        }

        public async Task<WorkflowTaskDTO> GetWorkflowTaskById(string workflowTaskId)
        {
            var workflowTask = await _repository.GetWorkflowTaskById(workflowTaskId);
            return _mapper.Map<WorkflowTaskDTO>(workflowTask);
        }

        public async Task CreateWorkflowTask(CreateUpdateWorkflowTaskDTO createUpdateWorkflowTaskDTO, bool AcceptResovledTask)
        {
            var workflow = await _workflowRepository.GetWorkflowById(createUpdateWorkflowTaskDTO.WorkflowId);
            if (workflow == null)
            {
                throw new AppException($"Workflow with id {createUpdateWorkflowTaskDTO.WorkflowId} not found");
            }
            if (createUpdateWorkflowTaskDTO.Status == StatusWorkflowTaskEnum.Resolved.ToString() && !AcceptResovledTask)
            {
                throw new AppException($"Workflow already have resolved Task");
            }
            await ValidationUserGroup(createUpdateWorkflowTaskDTO);
            var workflowTask = _mapper.Map<WorkflowTask>(createUpdateWorkflowTaskDTO);
            workflowTask.WorkflowTaskId = await GetNextId();
            workflowTask.CreatedDate = DateTime.Now;
            await _repository.AddWorkflowTask(workflowTask);
        }

        public async Task UpdateWorkflowTask(string workflowTaskId, CreateUpdateWorkflowTaskDTO createUpdateWorkflowTaskDTO)
        {
            WorkflowTask workflowTask = await _repository.GetWorkflowTaskById(workflowTaskId);
            if (workflowTask == null)
            {
                throw new AppException("WorkflowTask not found");
            }
            if (_workflowRepository.GetWorkflowById(createUpdateWorkflowTaskDTO.WorkflowId) == null)
            {
                throw new AppException("Workflow with id " + createUpdateWorkflowTaskDTO.WorkflowId + " not found");
            }
            if (createUpdateWorkflowTaskDTO.Status == StatusWorkflowTaskEnum.Resolved.ToString())
            {
                throw new AppException($"Workflow already have resolved Task");
            }
            await ValidationUserGroup(createUpdateWorkflowTaskDTO);
            workflowTask = _mapper.Map(createUpdateWorkflowTaskDTO, workflowTask);
            await _repository.UpdateWorkflowTask(workflowTask);
        }

        private async Task ValidationUserGroup(CreateUpdateWorkflowTaskDTO createUpdateWorkflowTaskDTO)
        {
            if (!string.IsNullOrEmpty(createUpdateWorkflowTaskDTO.AssignerId))
            {
                var user = await _userRepository.GetUserDetails(createUpdateWorkflowTaskDTO.AssignerId);
                if (user == null)
                {
                    throw new AppException($"User with id {createUpdateWorkflowTaskDTO.AssignerId} not found");
                }
            }
            if (!string.IsNullOrEmpty(createUpdateWorkflowTaskDTO.GroupId))
            {
                var group = await _groupRepository.GetGroupById(createUpdateWorkflowTaskDTO.GroupId);
                if (group == null)
                {
                    throw new AppException($"Group with id {createUpdateWorkflowTaskDTO.GroupId} not found");
                }
                if (!string.IsNullOrEmpty(createUpdateWorkflowTaskDTO.AssignerId))
                {
                    var userInGroup = group.Users.FirstOrDefault(u => u.UserId == createUpdateWorkflowTaskDTO.AssignerId);
                    if (userInGroup == null)
                    {
                        throw new AppException($"User with id {createUpdateWorkflowTaskDTO.AssignerId} not in group");
                    }
                }
            }

        }

        public async Task DeleteWorkflowTask(string workflowTaskId)
        {
            var workflowTask = await _repository.GetWorkflowTaskById(workflowTaskId);
            if (workflowTask == null)
            {
                throw new AppException($"Workflow Task with id {workflowTaskId} not found");
            }
            var listWorkflowAssignment = await _workflowAssignmentRepository.GetWorkflowAssignmentsByWorkflowTaskId(workflowTaskId);
            await _workflowAssignmentService.DeleteListWorkflowAssignment(listWorkflowAssignment);
            await _repository.DeleteWorkflowTask(workflowTask);
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
