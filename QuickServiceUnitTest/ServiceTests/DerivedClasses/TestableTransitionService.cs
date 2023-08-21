using AutoMapper;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Services.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickServiceUnitTest.ServiceTests.DerivedClasses
{
    public class TestableTransitionService : WorkflowTransitionService
    {
        public TestableTransitionService(
       IWorkflowTransitionRepository repository,
       IMapper mapper,
       IWorkflowTaskRepository workflowTaskRepository,
       IWorkflowRepository workflowRepository)
       : base(repository, workflowTaskRepository, mapper, workflowRepository)
        {
        }
    }
}
