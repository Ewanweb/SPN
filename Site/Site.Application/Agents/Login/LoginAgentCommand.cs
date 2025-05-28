using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Site.Application._shared;

namespace Site.Application.Agents.Login
{
    public record LoginAgentCommand(string Email, string Password) : IRequest<OperationResult>;
}
