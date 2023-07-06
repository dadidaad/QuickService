﻿using AutoMapper;
using QuickServiceWebAPI.DTOs.Workflow;
using QuickServiceWebAPI.DTOs.WorkflowStep;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class WorkflowService : IWorkflowService
    {
        private readonly IWorkflowRepository _repository;
        private readonly IMapper _mapper;
        public WorkflowService(IWorkflowRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<WorkflowDTO> GetWorkflows()
        {
            var workflows = _repository.GetWorkflows();
            return workflows.Select(workflow => _mapper.Map<WorkflowDTO>(workflow)).ToList();
        }

        public async Task<WorkflowDTO> GetWorkflowById(string workflowId)
        {
            var workflow = await _repository.GetWorkflowById(workflowId);
            return _mapper.Map<WorkflowDTO>(workflow);
        }

        public async Task CreateWorkflow(CreateUpdateWorkflowDTO createUpdateWorkflowDTO)
        {
            var workflow = _mapper.Map<Workflow>(createUpdateWorkflowDTO);
            workflow.WorkflowId = await GetNextId();
            workflow.CreatedAt = DateTime.Now;
            await _repository.AddWorkflow(workflow);
        }

        public async Task UpdateWorkflow(string workflowId, CreateUpdateWorkflowDTO createUpdateWorkflowDTO)
        {
            Workflow workflow = await _repository.GetWorkflowById(workflowId);
            if (workflow == null)
            {
                throw new AppException("Workflow not found");
            }
            if (!String.IsNullOrEmpty(createUpdateWorkflowDTO.WorkflowName))
            {
                workflow.WorkflowName = createUpdateWorkflowDTO.WorkflowName;
            }
            if (!String.IsNullOrEmpty(createUpdateWorkflowDTO.Description))
            {
                workflow.Description = createUpdateWorkflowDTO.Description;
            }
            if (!String.IsNullOrEmpty(createUpdateWorkflowDTO.CreatedBy))
            {
                workflow.CreatedBy = createUpdateWorkflowDTO.CreatedBy;
            }
            workflow = _mapper.Map<CreateUpdateWorkflowDTO, Workflow>(createUpdateWorkflowDTO, workflow);
            await _repository.UpdateWorkflow(workflow);
        }

        public async Task DeleteWorkflow(string workflowId)
        {
            var workflow = await _repository.GetWorkflowById(workflowId);
            await _repository.DeleteWorkflow(workflow);
        }
        public async Task<string> GetNextId()
        {
            Workflow lastWorkflow = await _repository.GetLastWorkflow();
            int id = 0;
            if (lastWorkflow == null)
            {
                id = 1;
            }
            else
            {
                id = IDGenerator.ExtractNumberFromId(lastWorkflow.WorkflowId) + 1;
            }
            string workflowId = IDGenerator.GenerateWorkflowId(id);
            return workflowId;
        }
    }
}
