using JobCandidateHub.API.Controllers;
using JobCandidateHub.Application.DTO.Response.Common;
using JobCandidateHub.Application.Manager.Implementation;
using JobCandidateHub.Application.Manager.Interface;
using JobCandidateHub.Domain.Entities;
using JobCandidateHub.Domain.Enum;
using JobCandidateHub.Domain.Interface;
using JobCandidateHub.UnitTest.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.UnitTest.Application
{
    public class CandidateManagerTest
    {
        private readonly CandidateManager _manager;
        private readonly Mock<ICandidateService> _service = new Mock<ICandidateService>();
        private readonly Mock<ILogger<CandidateManager>> _logger = new Mock<ILogger<CandidateManager>>();
        public CandidateManagerTest()
        {
            _manager = new CandidateManager(_service.Object,
                _logger.Object
                );
        }
        [Fact]
        public async Task AddOrUpdateJobCandidateDetails_OnSuccess_WhenAdd_ReturnTrue()
        {
            //arrange
            JobCandidateDataInfo.Init();
            var request = JobCandidateDataInfo.JobCandidateRequest;
            var addData = JobCandidateDataInfo.Candidate;
            addData.Id = 0;

            int candidateId = 1;
            var expected_result = new CommonApiResponse()
            {
                Data = true,
                Message = "Successfully added job candidate",
                Status = StatusType.Ok
            };
            _service.Setup(x => x.CheckCandidateDetailExist(request.Email)).ReturnsAsync(false);
            _service.Setup(x => x.AddCandidateDetail(It.IsAny<ECandidate>())).ReturnsAsync(candidateId);
            //act
            var actual_result = await _manager.AddOrUpdateJobCandidateDetails(request);
            //assert
            Assert.Equivalent(expected_result, actual_result);
        }
        [Fact]
        public async Task AddOrUpdateJobCandidateDetails_OnFailure_WhenAdd_ReturnFalse()
        {
            //arrange
            JobCandidateDataInfo.Init();
            var request = JobCandidateDataInfo.JobCandidateRequest;
            var addData = JobCandidateDataInfo.Candidate;
            addData.Id = 0;

            int candidateId = 1;
            var expected_result = new CommonApiResponse()
            {
                Data = false,
                Message = "Failed to add job candidate",
                Status = StatusType.ProcessError
            };
            _service.Setup(x => x.CheckCandidateDetailExist(request.Email)).ReturnsAsync(false);
            _service.Setup(x => x.AddCandidateDetail(addData)).ReturnsAsync(candidateId);
            //act
            var actual_result = await _manager.AddOrUpdateJobCandidateDetails(request);
            //assert
            Assert.Equivalent(expected_result, actual_result);
        }
        
        [Fact]
        public async Task AddOrUpdateJobCandidateDetails_OnSuccess_WhenUpdate_ReturnTrue()
        {
            //arrange
            JobCandidateDataInfo.Init();
            var request = JobCandidateDataInfo.JobCandidateRequest;
            var updateData = JobCandidateDataInfo.Candidate;
            updateData.UpdatedDate = DateTime.Now;
            var expected_result = new CommonApiResponse()
            {
                Data = true,
                Message = "Successfully updated job candidate",
                Status = StatusType.Ok
            };
            _service.Setup(x => x.CheckCandidateDetailExist(request.Email)).ReturnsAsync(true);
            _service.Setup(x => x.GetCandidateByEmail(request.Email)).ReturnsAsync(updateData);
            _service.Setup(x => x.UpdateCandidateDetail(It.IsAny<ECandidate>())).ReturnsAsync(true);
            //act
            var actual_result = await _manager.AddOrUpdateJobCandidateDetails(request);
            //assert
            Assert.Equivalent(expected_result, actual_result);
        }
        [Fact]
        public async Task AddOrUpdateJobCandidateDetails_OnFailure_WhenUpdate_ReturnFalse()
        {
            //arrange
            JobCandidateDataInfo.Init();
            var request = JobCandidateDataInfo.JobCandidateRequest;
            var updateData = JobCandidateDataInfo.Candidate;
            updateData.UpdatedDate = DateTime.Now;

            var expected_result = new CommonApiResponse()
            {
                Data = false,
                Message = "Failed to update job candidate",
                Status = StatusType.ProcessError
            };
            _service.Setup(x => x.CheckCandidateDetailExist(request.Email)).ReturnsAsync(true);
            _service.Setup(x => x.GetCandidateByEmail(request.Email)).ReturnsAsync(updateData);
            _service.Setup(x => x.UpdateCandidateDetail(It.IsAny<ECandidate>())).ReturnsAsync(false);
            //act
            var actual_result = await _manager.AddOrUpdateJobCandidateDetails(request);
            //assert
            Assert.Equivalent(expected_result, actual_result);
        }
        
        [Fact]
        public async Task AddOrUpdateJobCandidateDetails_OnFailure_WhenUpdateNotFound_ReturnFalse()
        {
            //arrange
            JobCandidateDataInfo.Init();
            var request = JobCandidateDataInfo.JobCandidateRequest;
            var updateData = JobCandidateDataInfo.Candidate;
            updateData = null;

            var expected_result = new CommonApiResponse()
            {
                Data = false,
                Message = "Candidate not found for the provided email",
                Status = StatusType.ProcessError
            };
            _service.Setup(x => x.CheckCandidateDetailExist(request.Email)).ReturnsAsync(true);
            _service.Setup(x => x.GetCandidateByEmail(request.Email)).ReturnsAsync(updateData);
            //act
            var actual_result = await _manager.AddOrUpdateJobCandidateDetails(request);
            //assert
            Assert.Equivalent(expected_result, actual_result);
        }

        [Fact]
        public async Task AddOrUpdateJobCandidateDetails_OnFailure_WhenValidationEmptyModel_ReturnFalse()
        {
            //arrange
            JobCandidateDataInfo.Init();
            var request = JobCandidateDataInfo.JobCandidateRequest;
            request = null;

            var expected_result = new CommonApiResponse()
            {
                Data = false,
                Message = "Model is empty",
                Status = StatusType.ProcessError,
            };
          //act
            var actual_result = await _manager.AddOrUpdateJobCandidateDetails(request);
            //assert
            Assert.Equivalent(expected_result, actual_result);
        }
        [Fact]
        public async Task AddOrUpdateJobCandidateDetails_OnFailure_WhenValidationPhoneLength_ReturnFalse()
        {
            //arrange
            JobCandidateDataInfo.Init();
            var request = JobCandidateDataInfo.JobCandidateRequest;
            request.PhoneNumber = "100";

            var expected_result = new CommonApiResponse()
            {
                Data = false,
                Message = "Phone number must be 10 digits",
                Status = StatusType.ProcessError,
            };
          //act
            var actual_result = await _manager.AddOrUpdateJobCandidateDetails(request);
            //assert
            Assert.Equivalent(expected_result, actual_result);
        }
        [Fact]
        public async Task AddOrUpdateJobCandidateDetails_OnFailure_WhenValidationDefaultToTime_ReturnFalse()
        {
            //arrange
            JobCandidateDataInfo.Init();
            var request = JobCandidateDataInfo.JobCandidateRequest;
            request.FromTime = "09:00";
            request.ToTime= "string";

            var expected_result = new CommonApiResponse()
            {
                Data = false,
                Message = "Both FromTime and ToTime are required when one is provided",
                Status = StatusType.ProcessError,
            };
          //act
            var actual_result = await _manager.AddOrUpdateJobCandidateDetails(request);
            //assert
            Assert.Equivalent(expected_result, actual_result);
        }
        [Fact]
        public async Task AddOrUpdateJobCandidateDetails_OnFailure_WhenValidationDefaultFromTime_ReturnFalse()
        {
            //arrange
            JobCandidateDataInfo.Init();
            var request = JobCandidateDataInfo.JobCandidateRequest;
            request.FromTime = "string";
            request.ToTime= "09:00";

            var expected_result = new CommonApiResponse()
            {
                Data = false,
                Message = "Both FromTime and ToTime are required when one is provided",
                Status = StatusType.ProcessError,
            };
          //act
            var actual_result = await _manager.AddOrUpdateJobCandidateDetails(request);
            //assert
            Assert.Equivalent(expected_result, actual_result);
        }
        [Fact]
        public async Task AddOrUpdateJobCandidateDetails_OnFailure_WhenValidationInvalidInterval_ReturnFalse()
        {
            //arrange
            JobCandidateDataInfo.Init();
            var request = JobCandidateDataInfo.JobCandidateRequest;
            request.FromTime = "02:00";
            request.ToTime= "01:00";

            var expected_result = new CommonApiResponse()
            {
                Data = false,
                Message = "ToTime must be greater than FromTime",
                Status = StatusType.ProcessError,
            };
          //act
            var actual_result = await _manager.AddOrUpdateJobCandidateDetails(request);
            //assert
            Assert.Equivalent(expected_result, actual_result);
        }
        [Fact]
        public async Task AddOrUpdateJobCandidateDetails_OnException_ReturnFalse()
        {
            //arrange
            JobCandidateDataInfo.Init();
            var request = JobCandidateDataInfo.JobCandidateRequest;

            var expected_result = new CommonApiResponse()
            {
                Data = false,
                Message = "Something went wrong",
                Status = StatusType.UnHandledException
            };
            _service.Setup(x => x.CheckCandidateDetailExist(request.Email)).ThrowsAsync(new Exception("Simulated exception"));
            //act
            var actual_result = await _manager.AddOrUpdateJobCandidateDetails(request);
            //assert
            Assert.Equivalent(expected_result, actual_result);
        }
    }
}
