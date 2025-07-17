using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Site.Application._shared;
using Site.Domain.Agents;

namespace Site.Application.Agents.AddFeatures
{
    public record AddAgentFeatureCommand(
        Guid AgentId,
        string Title,
        int Percentage
        ) : IRequest<OperationResult>;

    public class AgentFeatureDto
    {
        public string Title { get; set; }
        public int Percentage { get; set; }
    }
}
