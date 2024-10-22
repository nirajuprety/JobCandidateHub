using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.Domain.Entities
{
    public class ECandidate
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public string LinkedInUrl { get; set; }
        public string GithubUrl {  get; set; }  
        public string Comment{  get; set; }
        public DateTime CreatedDate{ get; set; }
        public DateTime UpdatedDate{ get; set; }
    }
}
