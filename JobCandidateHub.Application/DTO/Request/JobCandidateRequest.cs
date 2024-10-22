using JobCandidateHub.Application.Helper;
using System.ComponentModel.DataAnnotations;

namespace JobCandidateHub.Application.DTO.Request
{
    public class JobCandidateRequest
    {
        [Required(ErrorMessage = "Enter first name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Enter last name")]
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Enter email address")]
        [EmailAddress(ErrorMessage = "Enter valid email address")]
        public string Email { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        [UrlValidation(ErrorMessage = "Enter a valid LinkedIn profile url")]
        public string LinkedInUrl { get; set; }
        [UrlValidation(ErrorMessage = "Enter a valid Github profile url")]
        public string GithubUrl { get; set; }
        [Required(ErrorMessage = "Enter comment")]
        [StringLength(250, ErrorMessage = "The comment cannot exceed 250 characters.")]
        public string Comment { get; set; }


    }
}
