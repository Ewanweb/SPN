using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Site.Application._shared;

namespace Site.Application.Projects.RemoveProject
{
    public record RemoveProjectCommand(Guid ProjectId) : IRequest<OperationResult>;
}
