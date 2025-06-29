using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Site.Query.Projects.Dtos;

namespace Site.Query.Projects.GetById
{
    public record GetProjectByIdQuery(Guid ProjectId) : IRequest<ProjectDto>;
}
