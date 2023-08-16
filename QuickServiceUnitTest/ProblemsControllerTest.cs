using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using QuickServiceWebAPI.Controllers;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Repositories.Implements;
using QuickServiceWebAPI.Repositories;
using QuickServiceWebAPI.Services.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickServiceWebAPI.DTOs.Problem;
using Microsoft.AspNetCore.Mvc;
using QuickServiceWebAPI.DTOs.CustomField;

namespace QuickServiceUnitTest
{
    public class ProblemsControllerTest
    {
        private readonly ProblemsController _controller;
        public ProblemsControllerTest() 
        {
            var dbContext = new QuickServiceContext();
            var mockLogger = new Mock<ILogger<ProblemRepository>>();

            var problemService = new ProblemService(
                new ProblemRepository(dbContext, mockLogger.Object),
                new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Problem, ProblemDTO>();
                }).CreateMapper()
            );
            _controller = new ProblemsController(problemService);
        }

        [Fact]
        public void GetAllProblem_ReturnsOkResult()
        {
            var result = _controller.GetAllProblem();
            var resultType = result as OkObjectResult;
            var resultList = resultType.Value as List<ProblemDTO>;

            Assert.NotNull(result);
            Assert.IsType<List<ProblemDTO>>(resultType.Value);
            Assert.Equal(1, resultList.Count);
        }
    }
}
