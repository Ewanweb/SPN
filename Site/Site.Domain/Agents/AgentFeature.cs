using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Site.Domain._shared;
using Site.Domain.Agents.ValueObjects;

namespace Site.Domain.Agents
{
    public class AgentFeature : BaseEntity
    {
        public Guid AgentId { get; private set; }
        public string Title { get; private set; }
        public int Percentage { get; private set; }

        private AgentFeature() { }

        public AgentFeature(Guid agentId, string title, int percentage)
        {
            AgentId = agentId;
            Title = title;
            Percentage = percentage;
        }
    }
    
}
