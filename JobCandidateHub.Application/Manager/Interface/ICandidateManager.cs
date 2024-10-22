using JobCandidateHub.Application.DTO.Request;
using JobCandidateHub.Application.DTO.Response.Common;

namespace JobCandidateHub.Application.Manager.Interface
{
    public interface ICandidateManager
    {
        Task<CommonApiResponse> AddOrUpdateJobCandidateDetails(JobCandidateRequest request);
    }
}
