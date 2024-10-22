using JobCandidateHub.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.Application.DTO.Response.Common
{
    public class CommonApiResponse
    {
        public bool Data { get; set; }
        public string Message { get; set; } 
        public StatusType Status { get; set; }
    }
}
