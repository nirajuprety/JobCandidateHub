using JobCandidateHub.API.Services;
using JobCandidateHub.Application.DTO.Request;
using JobCandidateHub.Application.DTO.Response.Common;
using JobCandidateHub.Application.Manager.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JobCandidateHub.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class JobCandidateApiController : ControllerBase
    {
        private readonly ICandidateManager _candidateManager;
        private readonly ILogger<JobCandidateApiController> _logger;

        public JobCandidateApiController(ICandidateManager candidateManager,
            ILogger<JobCandidateApiController> logger)
        {
            _candidateManager = candidateManager;
            _logger = logger;
        }
        [HttpPost("AddOrUpdateJobCandidateDetail")]
        [ServiceFilter(typeof(InputValidationFilter))]
        public async Task<CommonApiResponse> AddUpdateJobCandidateDetails(JobCandidateRequest request)
        {
            _logger.LogInformation("Add or update job candidate intiated for request:" + JsonConvert.SerializeObject(request));
            var response = await _candidateManager.AddOrUpdateJobCandidateDetails(request);
            return response;
        }
    }
}
