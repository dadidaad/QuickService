using AutoMapper;
using QuickServiceWebAPI.DTOs.Problem;
using QuickServiceWebAPI.DTOs.Query;
using QuickServiceWebAPI.DTOs.RequestTicket;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Repositories.Implements;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Services.Implements
{
    public class ProblemService : IProblemService
    {
        private readonly IProblemRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ISlaService _slaService;
        private readonly IRequestTicketRepository _requestTicketRepository;
        private readonly IAttachmentService _attachmentService;

        public ProblemService(IProblemRepository repository, IMapper mapper, IUserRepository userRepository, 
            ISlaService slaService, IRequestTicketRepository requestTicketRepository, IAttachmentService attachmentService)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
            _slaService = slaService;
            _requestTicketRepository = requestTicketRepository;
            _attachmentService = attachmentService;
        }

        public async Task<ProblemDTO> CreateProblem(CreateProblemDTO createProblemDTO)
        {
            await ValidateData(createProblemDTO);
            var problem = _mapper.Map<Problem>(createProblemDTO);
            problem.ProblemId = await GetNextId();
            problem.Status = StatusEnum.Open.ToString();
            problem.Priority = PriorityEnum.Low.ToString();
            problem.Impact = ImpactEnum.Low.ToString();
            problem.CreatedTime = DateTime.Now;
            await _repository.AddProblem(problem);
            await AddProblemIdIdToRequestTicket(problem.ProblemId, createProblemDTO.RequestTicketIds);


            var createdProblemDTO = await _repository.GetProblemById(problem.ProblemId);
            return _mapper.Map<ProblemDTO>(createdProblemDTO);
        }

        public List<ProblemDTO> GetAllProblems()
        {
            var problems = _repository.GetProblems();
            return problems.Select(problem => _mapper.Map<ProblemDTO>(problem)).ToList();
        }

        public async Task<ProblemDTO> GetProblemById(string problemId)
        {
            var problem = await _repository.GetProblemById(problemId);
            if (problem == null)
            {
                throw new AppException("Can not found problem with ID: {problemId}", problemId);
            }
            return _mapper.Map<ProblemDTO>(problem);
        }

        public async Task UpdateProblem(UpdateProblemDTO updateProblemDTO)
        {
            var problem = await _repository.GetProblemById(updateProblemDTO.ProblemId);
            if (problem == null)
            {
                throw new AppException($"Problem with id: {updateProblemDTO.ProblemId} not found");
            }
            var assignee = await _userRepository.GetUserDetails(updateProblemDTO.AssigneeId);
            if (assignee == null)
            {
                throw new AppException($"User with id: {updateProblemDTO.AssigneeId} not found");
            }
            if (updateProblemDTO.AttachmentFile is not null)
            {
                problem.Attachment = await _attachmentService.CreateAttachment(updateProblemDTO.AttachmentFile);
            }
            var updateChange = _mapper.Map(updateProblemDTO, problem);
            await _repository.UpdateProblem(updateChange);
        }

        private async Task ValidateData(CreateProblemDTO createProblemDTO)
        {
            var assigner = await _userRepository.GetUserDetails(createProblemDTO.AssigneeId);
            if (assigner == null)
            {
                throw new AppException($"User with id: {createProblemDTO.AssigneeId} not found");
            }
            var sla = await _slaService.GetSlaById(createProblemDTO.Slaid);
            if (sla == null)
            {
                throw new AppException($"Sla with id: {createProblemDTO.Slaid} not found");
            }
        }

        public async Task AddProblemIdIdToRequestTicket(string problemId, List<String> RequestTicketIds)
        {
            foreach (var requestTicketId in RequestTicketIds)
            {
                var requestTicket = await _requestTicketRepository.GetRequestTicketById(requestTicketId);
                if (requestTicket == null)
                {
                    throw new AppException($"Request ticket with id: {requestTicketId} not found");
                    
                }
                requestTicket.ProblemId = problemId;
                await _requestTicketRepository.UpdateRequestTicket(requestTicket);
            }
        }

        private async Task<string> GetNextId()
        {
            Problem lastProblem = await _repository.GetLastProblem();
            int id = 0;
            if (lastProblem == null)
            {
                id = 1;
            }
            else
            {
                id = IDGenerator.ExtractNumberFromId(lastProblem.ProblemId) + 1;
            }
            string changeId = IDGenerator.GenerateProblemId(id);
            return changeId;
        }

        //public async Task<List<TicketQueryAdminDTO>> GetRequestTicketsQueryAdmin(QueryDTO queryDto)
        //{
        //    return await _repository.GetRequestTicketsQueryAdmin(queryDto);
        //}
    }
}
