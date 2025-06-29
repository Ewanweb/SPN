using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Site.Domain.Projects;
using Site.Query.Projects.Dtos;

namespace Site.Query.Projects.GetByList
{
    public record GetProjectsByListQuery : IRequest<List<ProjectDto>>;
}
