using Castle.Core.Logging;
using JobCandidateHub.API.Controllers;
using JobCandidateHub.Application.DTO.Response.Common;
using JobCandidateHub.Application.Manager.Interface;
using JobCandidateHub.Domain.Enum;
using JobCandidateHub.UnitTest.Data;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.UnitTest.Api
{
    public class JobCandidateApiControllerTest
    {
        private readonly JobCandidateApiController _controller;
        private readonly Mock<ICandidateManager> _manager = new Mock<ICandidateManager>();
        private readonly Mock<ILogger<JobCandidateApiController>> _logger = new Mock<ILogger<JobCandidateApiController>>(); 
        public JobCandidateApiControllerTest()
        {
            _controller = new JobCandidateApiController(_manager.Object, 
                _logger.Object
                );
        }

        [Fact]
        public async Task AddOrUpdateJobCandidateDetail_OnSuccess_ReturnTrue()
        {
            //arrange
            JobCandidateDataInfo.Init();
            var request = JobCandidateDataInfo.JobCandidateRequest;
            var expected_result = new CommonApiResponse()
            {
                Data = true,
                Message = "Successfully added job candidate",
                Status = StatusType.Ok,
            };
            _manager.Setup(x => x.AddOrUpdateJobCandidateDetails(request)).ReturnsAsync(expected_result);
            //act
            var actual_result = await _controller.AddUpdateJobCandidateDetails(request);
            //assert
            Assert.Equivalent(expected_result, actual_result);
        }

    }
}
