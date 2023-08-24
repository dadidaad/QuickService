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
    public class TestableGroupService : GroupService
    {
            public TestableGroupService(
            IGroupRepository groupRepository,
            IMapper mapper,
            IUserRepository userRepository,
            IBusinessHourRepository businessHourRepository)
            : base(groupRepository, mapper, userRepository, businessHourRepository)
            {
            }
    }
}
