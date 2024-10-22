using JobCandidateHub.Application.DTO.Request;
using JobCandidateHub.Application.DTO.Response.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.Application.Manager.Interface
{
    public interface ICandidateManager
    {
        Task<CommonApiResponse> AddOrUpdateJobCandidateDetails(JobCandidateRequest request);
    }
}
