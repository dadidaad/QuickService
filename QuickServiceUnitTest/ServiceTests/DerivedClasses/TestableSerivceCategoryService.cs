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
    public class TestableServiceCategory : ServiceCategoryService
    {
        public TestableServiceCategory(
        IServiceCategoryRepository repository,
        IMapper mapper)
        : base(repository, mapper)
        {
        }
    }
}
