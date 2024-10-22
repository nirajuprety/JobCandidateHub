using JobCandidateHub.Domain.Entities;
using JobCandidateHub.Domain.Interface;
using JobCandidateHub.Domain.Interface.Core;

namespace JobCandidateHub.Infrastructure.Service
{
    public class CandidateService : ICandidateService
    {
        private readonly IServiceFactory _factory;

        public CandidateService(IServiceFactory factory)
        {
            _factory = factory;
        }

        public async Task<int> AddCandidateDetail(ECandidate entity)
        {
            var candidate = _factory.GetInstance<ECandidate>();
            var result = await candidate.AddAsync(entity);
            return result.Id;
        }
        public async Task<bool> UpdateCandidateDetail(ECandidate entity)
        {
            var candidate = _factory.GetInstance<ECandidate>();
            await candidate.UpdateAsync(entity);
            return true;
        }
        public async Task<bool> CheckCandidateDetailExist(string email)
        {
            var candidateList = await _factory.GetInstance<ECandidate>().ListAsync();
            bool isExist = candidateList.Any(x => x.Email == email);
            return isExist;
        }

        public async Task<ECandidate> GetCandidateByEmail(string email)
        {
            var candidateList = await _factory.GetInstance<ECandidate>().ListAsync();
            var result = candidateList.FirstOrDefault(x => x.Email == email);
            return result;
        }
    }
}
