using AutoMapper;
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
    public class TestableWorkflowService : WorkflowService
    {
        public TestableWorkflowService(IWorkflowRepository repository, 
            IUserRepository userRepository, 
            IMapper mapper, 
            IServiceItemRepository serviceItemRepository, 
            ISlaRepository slaRepository, 
            IWorkflowTaskService WorkflowTaskService, 
            IWorkflowAssignmentRepository workflowAssignmentRepository, 
            IRequestTicketRepository requestTicketRepository) 
            : base(repository, userRepository, mapper, serviceItemRepository, slaRepository, WorkflowTaskService, workflowAssignmentRepository, requestTicketRepository)
        {
        }
    }
}
