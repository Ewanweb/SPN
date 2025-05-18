using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Site.Domain.Agents.ValueObjects;

namespace Site.Domain.Agents
{
    public class AgentFeature
    {
        public AgentFeatureKey Key { get; private set; }
        public Guid AgentId { get; private set; }

        // 🔻 Navigation Property
        public Agent Agent { get; private set; }

        // Constructor for initialization
        public AgentFeature(AgentFeatureKey key, Guid agentId)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            AgentId = agentId;
        }

        // EF Core parameterless constructor
        private AgentFeature() { }
    }
}
