using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Site.Domain.Agents.Enums;
using Site.Query.Agents.Dtos;

namespace Site.Query.Agents.GetByStatus
{
    public record GetAgentByStatusQuery(AgentStatus Status) : IRequest<List<AgentDto>>;
}
