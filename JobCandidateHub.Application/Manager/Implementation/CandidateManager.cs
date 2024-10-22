using JobCandidateHub.Application.DTO.Request;
using JobCandidateHub.Application.DTO.Response.Common;
using JobCandidateHub.Application.Manager.Interface;
using JobCandidateHub.Domain.Entities;
using JobCandidateHub.Domain.Enum;
using JobCandidateHub.Domain.Interface;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.Application.Manager.Implementation
{
    public class CandidateManager: ICandidateManager
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
                    _logger.LogInformation("Model is empty" + JsonConvert.SerializeObject(request));
                    return isValidModel;
                }

                ECandidate parsedData = ParseJobCandidateDetails(request);
                bool isExist = await _candidateService.CheckCandidateDetailExist(request.Email);
                if (isExist)
                {
                    //update
                    parsedData.UpdatedDate = DateTime.Now;
                    var updateResponse = await _candidateService.UpdateCandidateDetail(parsedData);
                    if (!updateResponse)
                    {
                        _logger.LogInformation("Failed to update job candidate" + JsonConvert.SerializeObject(parsedData));
                        return new CommonApiResponse()
                        {
                            Data = false,
                            Message = "Failed to update job candidate",
                            Status = StatusType.ProcessError,
                        };
                    }
                    _logger.LogInformation("Successfully updated job candidate details" + JsonConvert.SerializeObject(parsedData));
                    return new CommonApiResponse()
                    {
                        Data = true,
                        Message = "Successfully updated job candidate",
                        Status = StatusType.Ok,
                    };


                }
                else
                {
                    //add
                    parsedData.CreatedDate = DateTime.Now;
                    var addResponse = await _candidateService.AddCandidateDetail(parsedData);
                    if (addResponse <= 0)
                    {
                        _logger.LogInformation("Failed to add job candidate" + JsonConvert.SerializeObject(parsedData));
                        return new CommonApiResponse()
                        {
                            Data = false,
                            Message = "Failed to add job candidate",
                            Status = StatusType.ProcessError,
                        };
                    }
                    _logger.LogInformation("Successfully added job candidate details" + JsonConvert.SerializeObject(parsedData));
                    return new CommonApiResponse()
                    {
                        Data = true,
                        Message = "Successfully added job candidate",
                        Status = StatusType.Ok,
                    };

                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Exception occured:" +ex.Message);
                return new CommonApiResponse()
                {
                    Data = false,
                    Message = "Something went wrong",
                    Status = StatusType.UnHandledException,
                };

            }
           
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
           if(request == null)
            {
                return new CommonApiResponse()
                {
                    Data = false,
                    Message = "Model is empty",
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
