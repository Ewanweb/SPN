using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Site.Application._shared;
using Site.Domain.Agents;
using Site.Domain.Agents.Enums;
using Site.Domain.Agents.ValueObjects;

namespace Site.Application.Agents.Edit
{
    public record EditAgentCommand(
        string Slug,
        string FullName,
        string GithubLink,
        string ImageName,
        string Description,
        string Email,
        string Password,
        AgentPhoneNumber PhoneNumber) : IRequest<OperationResult>;
}
