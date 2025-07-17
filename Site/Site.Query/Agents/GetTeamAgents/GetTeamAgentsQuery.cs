using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Site.Query.Agents.Dtos;

namespace Site.Query.Agents.GetTeamAgents
{
    public class GetTeamAgentsQuery : IRequest<List<AgentDto>>;
}
