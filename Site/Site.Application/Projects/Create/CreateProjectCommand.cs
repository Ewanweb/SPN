using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Site.Application._shared;

namespace Site.Application.Projects.Create
{
    public record CreateProjectCommand(string Title, string Description, IFormFile? Image) : IRequest<OperationResult>;
}
