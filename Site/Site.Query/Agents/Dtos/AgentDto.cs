using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Site.Domain.Agents;
using Site.Domain.Agents.Enums;
using Site.Domain.Agents.ValueObjects;

namespace Site.Query.Agents.Dtos
{
    public class AgentDto : IdentityUser
    {
        public string FullName { get;  set; }
        public string Slug { get;  set; }
        public string GithubLink { get;  set; }
        public string ImageName { get;  set; }
        public string Description { get;  set; }
        public string Email { get;  set; }
        public AgentPhoneNumber PhoneNumber { get;  set; }
        public string? ResumeFileName { get;  set; }
        public AgentStatus Status { get;  set; }
        public List<AgentFeature> AgentFeatures { get;  set; } = new();
    }

    public enum AgentStatusDto
    {
        [Display(Name = "فعال")]
        Active = 0,

        [Display(Name = "غیر فعال")]
        InActive = 1,

        [Display(Name = "ادمین")]
        Admin = 2,

        [Display(Name = "مدیر کل")]
        Owner = 3,
    }
}
