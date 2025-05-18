using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Site.Domain._shared;

namespace Site.Domain.Agents.ValueObjects
{
    public class AgentFeatureKey : ValueObject
    {
        public string Value { get; }

        public AgentFeatureKey(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Agent feature key cannot be empty.", nameof(value));

            Value = value.Trim();
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value.ToLower(); // برای مقایسه case-insensitive
        }

        public override string ToString() => Value;

    }
}
