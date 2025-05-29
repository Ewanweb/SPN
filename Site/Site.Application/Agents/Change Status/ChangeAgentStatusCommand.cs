using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Site.Application._shared;
using Site.Domain.Agents.Enums;

namespace Site.Application.Agents.Change_Status
{
    public record ChangeAgentStatusCommand(string Slug, AgentStatus Status) : IRequest<OperationResult>;
}
