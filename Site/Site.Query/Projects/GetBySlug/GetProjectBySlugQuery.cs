using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Site.Query.Projects.Dtos;

namespace Site.Query.Projects.GetBySlug
{
    public record GetProjectBySlugQuery(string Slug) : IRequest<ProjectDto>;
}
