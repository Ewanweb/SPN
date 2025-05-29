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
    public record AddAgentFeatureCommand(string Slug, List<string> Features) : IRequest<OperationResult>;
}
