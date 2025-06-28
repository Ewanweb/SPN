using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Site.Application._shared;

namespace Site.Application.Projects.RemoveImage
{
    public record RemoveImageCommand(Guid ProjectId, List<string>? ImageNames) : IRequest<OperationResult>;
}
