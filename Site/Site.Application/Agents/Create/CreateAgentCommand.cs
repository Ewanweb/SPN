using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Site.Application._shared;
using Site.Domain.Agents.ValueObjects;

namespace Site.Application.Agents.Create
{
    public record CreateAgentCommand(
        string FullName,
        string Email,
        string Password,
        string PhoneNumber
        ) : IRequest<OperationResult>;
}
