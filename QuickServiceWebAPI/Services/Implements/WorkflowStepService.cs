using AutoMapper;
using QuickServiceWebAPI.DTOs.WorkflowStep;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class WorkflowStepService : IWorkflowStepService
    {
        private readonly IWorkflowStepRepository _repository;
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        public WorkflowStepService(IWorkflowStepRepository repository,
            IWorkflowRepository workflowRepository, IMapper mapper, 
            IUserRepository userRepository, IGroupRepository groupRepository)
        {
            _repository = repository;
            _workflowRepository = workflowRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _groupRepository = groupRepository;
        }

        public List<WorkflowStepDTO> GetWorkflowsStep()
        {
            var workflowSteps = _repository.GetWorkflowSteps();
            return workflowSteps.Select(workflowStep => _mapper.Map<WorkflowStepDTO>(workflowStep)).ToList();
        }

        public async Task<WorkflowStepDTO> GetWorkflowStepById(string workflowStepId)
        {
            var workflowStep = await _repository.GetWorkflowStepById(workflowStepId);
            return _mapper.Map<WorkflowStepDTO>(workflowStep);
        }

        public async Task CreateWorkflowStep(CreateUpdateWorkflowStepDTO createUpdateWorkflowStepDTO)
        {
            await ValidationUserGroup(createUpdateWorkflowStepDTO);
            var workflowStep = _mapper.Map<WorkflowStep>(createUpdateWorkflowStepDTO);
            workflowStep.WorkflowStepId = await GetNextId();
            workflowStep.CreatedDate = DateTime.Now;
            await _repository.AddWorkflowStep(workflowStep);
        }
 
        public async Task UpdateWorkflowStep(string workflowStepId, CreateUpdateWorkflowStepDTO CreateUpdateWorkflowStepDTO)
        {
            WorkflowStep workflowStep = await _repository.GetWorkflowStepById(workflowStepId);
            if (workflowStep == null)
            {
                throw new AppException("WorkflowStep not found");
            }
            if (_workflowRepository.GetWorkflowById(CreateUpdateWorkflowStepDTO.WorkflowId) == null)
            {
                throw new AppException("Workflow with id " + CreateUpdateWorkflowStepDTO.WorkflowId + " not found");
            }
            workflowStep = _mapper.Map(CreateUpdateWorkflowStepDTO, workflowStep);
            await _repository.UpdateWorkflowStep(workflowStep);
        }

        private async Task ValidationUserGroup(CreateUpdateWorkflowStepDTO createUpdateWorkflowStepDTO)
        {
            if(!string.IsNullOrEmpty(createUpdateWorkflowStepDTO.AssignerId))
            {
                var user = await _userRepository.GetUserDetails(createUpdateWorkflowStepDTO.AssignerId);
                if(user == null)
                {
                    throw new AppException($"User with id {createUpdateWorkflowStepDTO.AssignerId} not found");
                }
            }
            if (!string.IsNullOrEmpty(createUpdateWorkflowStepDTO.GroupId))
            {
                var group = await _groupRepository.GetGroupById(createUpdateWorkflowStepDTO.GroupId);
                if (group == null)
                {
                    throw new AppException($"Group with id {createUpdateWorkflowStepDTO.GroupId} not found");
                }
                if (!string.IsNullOrEmpty(createUpdateWorkflowStepDTO.AssignerId))
                {
                    var userInGroup = group.Users.FirstOrDefault(u => u.UserId == createUpdateWorkflowStepDTO.AssignerId);
                    if(userInGroup == null)
                    {
                        throw new AppException($"User with id {createUpdateWorkflowStepDTO.AssignerId} not in group");
                    }
                }
            }

        }
        public async Task DeleteWorkflowStep(string workflowStepId)
        {

        }
        public async Task<string> GetNextId()
        {
            WorkflowStep lastWorkflowStep = await _repository.GetLastWorkflowStep();
            int id = 0;
            if (lastWorkflowStep == null)
            {
                id = 1;
            }
            else
            {
                id = IDGenerator.ExtractNumberFromId(lastWorkflowStep.WorkflowStepId) + 1;
            }
            string workflowStepId = IDGenerator.GenerateWorkflowStepId(id);
            return workflowStepId;
        }
    }
}
