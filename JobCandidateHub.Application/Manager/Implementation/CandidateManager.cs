using JobCandidateHub.Application.DTO.Request;
using JobCandidateHub.Application.DTO.Response.Common;
using JobCandidateHub.Application.Manager.Interface;
using JobCandidateHub.Domain.Entities;
using JobCandidateHub.Domain.Enum;
using JobCandidateHub.Domain.Interface;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace JobCandidateHub.Application.Manager.Implementation
{
    public class CandidateManager : ICandidateManager
    {
        private readonly ICandidateService _candidateService;
        private readonly ILogger<CandidateManager> _logger;
        public CandidateManager(ICandidateService candidateService, ILogger<CandidateManager> logger)
        {
            _logger = logger;
            _candidateService = candidateService;
        }

        public async Task<CommonApiResponse> AddOrUpdateJobCandidateDetails(JobCandidateRequest request)
        {
            try
            {
                var isValidModel = VaidateModel(request);
                if (!isValidModel.Data)
                {
                    _logger.LogWarning("Invalid model: " + JsonConvert.SerializeObject(request));
                    return isValidModel;
                }

                var parsedData = ParseJobCandidateDetails(request);
                var isExist = await _candidateService.CheckCandidateDetailExist(request.Email);

                if (isExist)
                {
                    return await UpdateCandidateDetails(parsedData, request.Email);
                }
                else
                {
                    return await AddCandidateDetails(parsedData);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occurred while adding/updating candidate: " + ex);
                return new CommonApiResponse
                {
                    Data = false,
                    Message = "Something went wrong",
                    Status = StatusType.UnHandledException
                };
            }
        }

        private async Task<CommonApiResponse> UpdateCandidateDetails(ECandidate parsedData, string email)
        {
            var existingCandidate = await _candidateService.GetCandidateByEmail(email);
            if (existingCandidate != null)
            {
                parsedData.Id = existingCandidate.Id;
                parsedData.UpdatedDate = DateTime.Now;

                var updateResponse = await _candidateService.UpdateCandidateDetail(parsedData);
                if (!updateResponse)
                {
                    _logger.LogInformation("Failed to update job candidate: " + JsonConvert.SerializeObject(parsedData));
                    return new CommonApiResponse
                    {
                        Data = false,
                        Message = "Failed to update job candidate",
                        Status = StatusType.ProcessError
                    };
                }

                _logger.LogInformation("Successfully updated job candidate: " + JsonConvert.SerializeObject(parsedData));
                return new CommonApiResponse
                {
                    Data = true,
                    Message = "Successfully updated job candidate",
                    Status = StatusType.Ok
                };
            }

            _logger.LogInformation("Candidate not found for email: " + email);
            return new CommonApiResponse
            {
                Data = false,
                Message = "Candidate not found for the provided email",
                Status = StatusType.ProcessError
            };
        }

        private async Task<CommonApiResponse> AddCandidateDetails(ECandidate parsedData)
        {
            parsedData.CreatedDate = DateTime.Now;

            var addResponse = await _candidateService.AddCandidateDetail(parsedData);
            if (addResponse <= 0)
            {
                _logger.LogError("Failed to add job candidate: " + JsonConvert.SerializeObject(parsedData));
                return new CommonApiResponse
                {
                    Data = false,
                    Message = "Failed to add job candidate",
                    Status = StatusType.ProcessError
                };
            }

            _logger.LogInformation("Successfully added job candidate: " + JsonConvert.SerializeObject(parsedData));
            return new CommonApiResponse
            {
                Data = true,
                Message = "Successfully added job candidate",
                Status = StatusType.Ok
            };
        }



        private ECandidate ParseJobCandidateDetails(JobCandidateRequest request)
        {
            var result = new ECandidate()
            {
                Comment = request.Comment,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                FromTime = request.FromTime,
                ToTime = request.ToTime,
                GithubUrl = request.GithubUrl,
                LinkedInUrl = request.LinkedInUrl,
                PhoneNumber = request.PhoneNumber,
            };
            return result;
        }

        private CommonApiResponse VaidateModel(JobCandidateRequest request)
        {
            if (request == null)
            {
                return new CommonApiResponse()
                {
                    Data = false,
                    Message = "Model is empty",
                    Status = StatusType.ProcessError,
                };
            }

            if (!string.IsNullOrEmpty(request.PhoneNumber) && request.PhoneNumber.Length != 10)
            {
                return new CommonApiResponse()
                {
                    Data = false,
                    Message = "Phone number must be 10 digits",
                    Status = StatusType.ProcessError,
                };
            }
            if ((request.FromTime != default &&
                request.ToTime == default) ||
                (request.FromTime == default &&
                request.ToTime != default))
            {
                return new CommonApiResponse()
                {
                    Data = false,
                    Message = "Both FromTime and ToTime are required when one is provided",
                    Status = StatusType.ProcessError,
                };
            }
            if (request.FromTime != default &&
                request.ToTime != default &&
                request.ToTime <= request.FromTime)
            {
                return new CommonApiResponse()
                {
                    Data = false,
                    Message = "ToTime must be greater than FromTime",
                    Status = StatusType.ProcessError,
                };
            }
            return new CommonApiResponse()
            {
                Data = true,
                Message = "valid model",
                Status = StatusType.Ok,
            };
        }
    }
}
