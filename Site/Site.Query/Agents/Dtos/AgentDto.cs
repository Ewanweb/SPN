using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Site.Application.Agents.AddFeatures;
using Site.Domain.Agents;
using Site.Domain.Agents.Enums;
using Site.Domain.Agents.ValueObjects;

namespace Site.Query.Agents.Dtos
{
    public class AgentDto : BaseDto
    {
             public string FullName { get;  set; }
        public string Slug { get;  set; }
        public string GithubLink { get;  set; }
        public string ImageName { get;  set; }
        public string Description { get;  set; }
        public string Email { get;  set; }
        public string? Profienece { get;  set; }
        public string? MyProfienece { get;  set; }
        public string? Experience { get;  set; }
        public AgentPhoneNumber PhoneNumber { get;  set; }
        public string? ResumeFileName { get;  set; }
        public AgentStatus Status { get;  set; }
        public List<AgentFeatureDto> AgentFeatures { get;  set; } = new();
    }


    
}
