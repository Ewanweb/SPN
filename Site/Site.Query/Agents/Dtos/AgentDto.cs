using System;
using System.Collections.Generic;
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
        public string FullName { get; private set; }
        public string Slug { get; private set; }
        public string GithubLink { get; private set; }
        public string ImageName { get; private set; }
        public string Description { get; private set; }
        public string Email { get; private set; }
        public AgentPhoneNumber PhoneNumber { get; private set; }
        public string? ResumeFileName { get; private set; }
        public AgentStatus Status { get; private set; }
        public List<AgentFeature> AgentFeatures { get; private set; } = new();
    }
}
