using AutoMapper;
using Microsoft.Extensions.Logging;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Services;
using QuickServiceWebAPI.Services.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickServiceUnitTest.ServiceTests.DerivedClasses
{
    public class TestableRequestTicketService : RequestTicketService
    {
        public TestableRequestTicketService(IRequestTicketRepository requestTicketRepository, 
            ILogger<RequestTicketService> logger, 
            IMapper mapper, 
            IUserRepository userRepository, 
            IServiceItemRepository serviceItemRepository, 
            IAttachmentService attachmentService, 
            ISlaRepository slaRepository, 
            IWorkflowAssignmentService workflowAssignmentService, 
            IRequestTicketHistoryService requestTicketHistoryService, 
            IRequestTicketHistoryRepository requestTicketHistoryRepository, 
            IQueryRepository queryRepository) 
            : base(requestTicketRepository, logger, mapper, userRepository, serviceItemRepository, attachmentService, slaRepository, workflowAssignmentService, requestTicketHistoryService, requestTicketHistoryRepository, queryRepository)
        {

        }
    }
}
