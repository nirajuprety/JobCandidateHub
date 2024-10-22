using JobCandidateHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.Domain.Interface
{
    public interface ICandidateService
    {
        public Task<int> AddCandidateDetail(ECandidate entity);
        public Task<bool> UpdateCandidateDetail(ECandidate entity);
        public Task<bool> CheckCandidateDetailExist(string email);
        Task<ECandidate> GetCandidateByEmail(string email);
    }
}
