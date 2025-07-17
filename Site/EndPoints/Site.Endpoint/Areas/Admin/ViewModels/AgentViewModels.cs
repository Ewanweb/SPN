using System.ComponentModel.DataAnnotations;
using Site.Domain.Agents.ValueObjects;

namespace Site.Endpoint.Areas.Admin.ViewModels
{
    public class AgentViewModels
    {

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }

    public class EditAgentViewModel
    {
        public string FullName { get; set; }
        public string Slug { get; set; }
        public string? Profienece { get; set; }
        public string? MyProfienece { get; set; }
        public string? Experience { get; set; }
        public string GithubLink { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public AgentPhoneNumber PhoneNumber { get; set; }
        public IFormFile? ResumeFileName { get; set; }
    }
}
