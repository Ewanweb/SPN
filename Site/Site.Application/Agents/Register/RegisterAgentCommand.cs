using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Site.Application._shared;
using Site.Domain.Agents.ValueObjects;

namespace Site.Application.Agents.Register
{
    public record RegisterAgentCommand(string FullName, string Email, string Password, string PhoneNumber)
        : IRequest<OperationResult>;
}
