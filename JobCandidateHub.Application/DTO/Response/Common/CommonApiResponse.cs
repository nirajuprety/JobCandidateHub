using JobCandidateHub.Domain.Enum;

namespace JobCandidateHub.Application.DTO.Response.Common
{
    public class CommonApiResponse
    {
        public bool Data { get; set; }
        public string Message { get; set; }
        public StatusType Status { get; set; }
    }
}
