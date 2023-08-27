using AutoMapper;
using QuickServiceWebAPI.DTOs.Problem;
using QuickServiceWebAPI.DTOs.Query;
using QuickServiceWebAPI.DTOs.RequestTicket;
using QuickServiceWebAPI.Models.Enums;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Repositories.Implements;
using QuickServiceWebAPI.Utilities;
using QuickServiceWebAPI.DTOs.Change;

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
        private readonly IRequestTicketHistoryService _requestTicketHistoryService;
        private readonly IRequestTicketHistoryRepository _requestTicketHistoryRepository;

        public ProblemService(IProblemRepository repository, IMapper mapper, IUserRepository userRepository, 
            ISlaService slaService, IRequestTicketRepository requestTicketRepository, IAttachmentService attachmentService,
            IRequestTicketHistoryService requestTicketHistoryService, IRequestTicketHistoryRepository requestTicketHistoryRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
            _slaService = slaService;
            _requestTicketRepository = requestTicketRepository;
            _attachmentService = attachmentService;
            _requestTicketHistoryService = requestTicketHistoryService;
            _requestTicketHistoryRepository = requestTicketHistoryRepository;
        }

        public async Task<ProblemDTO> CreateProblem(CreateProblemDTO createProblemDTO)
        {
            var requester = await _userRepository.GetUserDetails(createProblemDTO.RequesterId);
            if (requester == null)
            {
                throw new AppException($"User with id {createProblemDTO.RequesterId} not found");
            }
            await ValidateData(createProblemDTO);
            var problem = _mapper.Map<Problem>(createProblemDTO);
            problem.ProblemId = await GetNextId();
            problem.Status = StatusEnum.Open.ToString();
            problem.Priority = PriorityEnum.Low.ToString();
            problem.Impact = ImpactEnum.Low.ToString();
            problem.CreatedTime = DateTime.Now;
            problem.RequesterId = requester.UserId;
            await _repository.AddProblem(problem);
            await AddProblemIdIdToRequestTicket(problem.ProblemId, createProblemDTO.RequestTicketIds);

            var historyFirst = new RequestTicketHistory
            {

                RequestTicketHistoryId = await _requestTicketHistoryService.GetNextIdRequestTicketHistory(),
                ProblemId = problem.ProblemId,
                Content = $"Create problem",
                LastUpdate = problem.CreatedTime,
                UserId = problem.RequesterId
            };
            await _requestTicketHistoryRepository.AddRequestTicketHistory(historyFirst);

            var historySecond = new RequestTicketHistory
            {

                RequestTicketHistoryId = await _requestTicketHistoryService.GetNextIdRequestTicketHistory(),
                ProblemId = problem.ProblemId,
                Content = $"Assigned to",
                LastUpdate = problem.CreatedTime,
                UserId = problem.AssigneeId
            };
            await _requestTicketHistoryRepository.AddRequestTicketHistory(historySecond);

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
            var existingProblem = await _repository.GetProblemById(updateProblemDTO.ProblemId);
            if (existingProblem == null)
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
                existingProblem.Attachment = await _attachmentService.CreateAttachment(updateProblemDTO.AttachmentFile);
            }

            if (existingProblem.AssigneeId != updateProblemDTO.AssigneeId)
            {
                var history = new RequestTicketHistory();
                history.Content = $"Assigned to";
                history.RequestTicketHistoryId = await _requestTicketHistoryService.GetNextIdRequestTicketHistory();
                history.ProblemId = updateProblemDTO.ProblemId;
                history.LastUpdate = DateTime.Now;
                history.UserId = updateProblemDTO.AssigneeId;
                await _requestTicketHistoryRepository.AddRequestTicketHistory(history);
            }


            if (updateProblemDTO.Status == StatusEnum.Resolved.ToString())
            {
                var history = new RequestTicketHistory();
                history.Content = $"Problem is Completed";
                history.RequestTicketHistoryId = await _requestTicketHistoryService.GetNextIdRequestTicketHistory();
                history.ProblemId = existingProblem.ProblemId;
                history.LastUpdate = DateTime.Now;
                history.UserId = existingProblem.AssigneeId;
                await _requestTicketHistoryRepository.AddRequestTicketHistory(history);
            }

            if (existingProblem.Status != updateProblemDTO.Status && updateProblemDTO.Status != StatusEnum.Resolved.ToString())
            {
                var history = new RequestTicketHistory();
                history.Content = $"Change Status to {updateProblemDTO.Status}";
                history.RequestTicketHistoryId = await _requestTicketHistoryService.GetNextIdRequestTicketHistory();
                history.ProblemId = existingProblem.ProblemId;
                history.LastUpdate = DateTime.Now;
                history.UserId = existingProblem.AssigneeId;
                await _requestTicketHistoryRepository.AddRequestTicketHistory(history);
            }

            if (existingProblem.Impact != updateProblemDTO.Impact)
            {
                var history = new RequestTicketHistory();
                history.Content = $"Change Impact to {updateProblemDTO.Impact}";
                history.RequestTicketHistoryId = await _requestTicketHistoryService.GetNextIdRequestTicketHistory();
                history.ProblemId = existingProblem.ProblemId;
                history.LastUpdate = DateTime.Now;
                history.UserId = existingProblem.AssigneeId;
                await _requestTicketHistoryRepository.AddRequestTicketHistory(history);
            }

            if (existingProblem.Priority != updateProblemDTO.Priority)
            {
                var history = new RequestTicketHistory();
                history.Content = $"Change Priority to {updateProblemDTO.Priority}";
                history.RequestTicketHistoryId = await _requestTicketHistoryService.GetNextIdRequestTicketHistory();
                history.ProblemId = updateProblemDTO.ProblemId;
                history.LastUpdate = DateTime.Now;
                history.UserId = updateProblemDTO.AssigneeId;
                await _requestTicketHistoryRepository.AddRequestTicketHistory(history);
            }

            var updateChange = _mapper.Map(updateProblemDTO, existingProblem);
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
                if (!string.IsNullOrEmpty(requestTicket.ProblemId))
                {
                    throw new AppException($"Request ticket already in another problem");

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
