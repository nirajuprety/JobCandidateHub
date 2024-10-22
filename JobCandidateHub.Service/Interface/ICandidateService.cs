using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.Domain.Interface
{
    public interface ICandidateService
    {
        public Task<int> AddCandidateDetail { get; set; }
        public Task<bool> UpdateCandidateDetail { get; set; }
    }
}
