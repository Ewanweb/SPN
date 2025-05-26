using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Site.Domain._shared;
using Site.Domain.Agents.ValueObjects;

namespace Site.Domain.Agents
{
    public class AgentFeature : ValueObject
    {
        public string Key { get; private set; }

        private AgentFeature() { }

        public AgentFeature(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            Key = key;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Key;
        }
    }
}
