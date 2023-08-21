using AutoMapper;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Repositories.Implements;
using QuickServiceWebAPI.Services;
using QuickServiceWebAPI.Services.Implements;
using QuickServiceWebAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickServiceUnitTest.ServiceTests.DerivedClasses
{
    public class TestableSlaSerivce : SlaService
    {
        public TestableSlaSerivce(
        ISlaRepository repository,
        IMapper mapper,
        ISlametricService slametricService,
        ISlametricRepository slametricRepository)
        : base(repository, mapper, slametricService, slametricRepository)
        {
        }
    }
}
