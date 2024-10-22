using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace JobCandidateHub.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class ECandidateHistory
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public TimeOnly FromTime { get; set; }
        public TimeOnly ToTime { get; set; }
        public string LinkedInUrl { get; set; }
        public string GithubUrl { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int CandidateId { get; set; }
    }
}
