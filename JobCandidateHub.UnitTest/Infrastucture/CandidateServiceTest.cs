using JobCandidateHub.Infrastructure.Repository;
using JobCandidateHub.Infrastructure.Service;
using JobCandidateHub.UnitTest.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.UnitTest.Infrastucture
{
    public class CandidateServiceTest : IClassFixture<DatabaseFixture>
    {
        [Fact]
        public async Task AddCandidateDetail_OnSuccess_ReturnsId()
        {
            DatabaseFixture _fixture = new DatabaseFixture();
            using (var factory = new JobCandidateHubServiceFactory(_fixture.mockDbContext, true))
            {
                //Arrange
                var service = new CandidateService(factory);
                var request = JobCandidateDataInfo.Candidate;
                request.Id = 0;
                var EXPECTED_RESULT = 2;
                //Act
                var actual_result = await service.AddCandidateDetail(request);
                //Assert
                Assert.Equivalent(EXPECTED_RESULT, actual_result);
            }
        }
        [Fact]
        public async Task UpdateCandidateDetail_OnSuccess_ReturnsTrue()
        {
            DatabaseFixture _fixture = new DatabaseFixture();
            using (var factory = new JobCandidateHubServiceFactory(_fixture.mockDbContext, true))
            {
                //Arrange
                var service = new CandidateService(factory);
                var request = JobCandidateDataInfo.Candidate;
               
                var EXPECTED_RESULT = true;
                //Act
                var actual_result = await service.UpdateCandidateDetail(request);
                //Assert
                Assert.Equivalent(EXPECTED_RESULT, actual_result);
            }
        }
        
        [Fact]
        public async Task CheckCandidateDetailExist_OnSuccess_ReturnsTrue()
        {
            DatabaseFixture _fixture = new DatabaseFixture();
            using (var factory = new JobCandidateHubServiceFactory(_fixture.mockDbContext, true))
            {
                //Arrange
                var service = new CandidateService(factory);
                var request = JobCandidateDataInfo.Candidate;
               
                var EXPECTED_RESULT = true;
                //Act
                var actual_result = await service.CheckCandidateDetailExist(request.Email);
                //Assert
                Assert.Equivalent(EXPECTED_RESULT, actual_result);
            }
        }
        [Fact]
        public async Task GetCandidateByEmail_OnSuccess_ReturnsTrue()
        {
            DatabaseFixture _fixture = new DatabaseFixture();
            using (var factory = new JobCandidateHubServiceFactory(_fixture.mockDbContext, true))
            {
                //Arrange
                var service = new CandidateService(factory);
                var request = JobCandidateDataInfo.Candidate;
               
                var EXPECTED_RESULT = request;
                //Act
                var actual_result = await service.GetCandidateByEmail(request.Email);
                //Assert
                Assert.Equivalent(EXPECTED_RESULT, actual_result);
            }
        }
    }
}
