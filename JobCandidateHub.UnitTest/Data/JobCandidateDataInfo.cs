using JobCandidateHub.Application.DTO.Request;
using JobCandidateHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.UnitTest.Data
{
    public static class JobCandidateDataInfo
    {
        public static void Init()
        {
            Candidate = new ECandidate()
            {
                Id =1,
                FirstName = "John",
                LastName = "Doe",
                Comment = "Hello",
                Email = "john@doe.com",
                FromTime = TimeOnly.MinValue,
                ToTime = TimeOnly.MinValue,
                GithubUrl = "https://sigma.software/",
                LinkedInUrl = "https://sigma.software/",
                PhoneNumber = "9876543210",
            };

            CandidatesList = new List<ECandidate>()
            {
                Candidate,
            };
            JobCandidateRequest = new JobCandidateRequest()
            {
                FirstName = "John",
                LastName = "Doe",
                Comment = "Hello",
                Email = "john@doe.com",
                FromTime = "08:00",
                ToTime = "09:00",
                GithubUrl = "https://sigma.software/",
                LinkedInUrl = "https://sigma.software/",
                PhoneNumber = "9876543210",
                
            };
        }
        public static List<ECandidate> CandidatesList { get; set; }
        public static ECandidate Candidate { get; set; }
        public static JobCandidateRequest JobCandidateRequest { get; set; }
    }
}
