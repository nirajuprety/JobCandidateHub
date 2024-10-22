using JobCandidateHub.Domain.Entities;

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
