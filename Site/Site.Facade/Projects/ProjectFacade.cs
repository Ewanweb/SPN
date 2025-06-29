using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Site.Application._shared;
using Site.Application.Agents.Login;
using Site.Application.Agents.Register;
using Site.Application.Projects.AddImages;
using Site.Application.Projects.Create;
using Site.Application.Projects.Edit;
using Site.Application.Projects.RemoveImage;
using Site.Application.Projects.RemoveProject;
using Site.Query.Agents.Dtos;
using Site.Query.Projects.Dtos;
using Site.Query.Projects.GetById;
using Site.Query.Projects.GetByList;
using Site.Query.Projects.GetBySlug;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Site.Facade.Projects
{
    public class ProjectFacade : IProjectFacade
    {
        private readonly IMediator _mediator;

        public ProjectFacade(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<OperationResult> AddImages(AddProjectImageCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> Create(CreateProjectCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> Edit(EditProjectCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> Remove(RemoveProjectCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> RemoveImage(RemoveImageCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<ProjectDto> GetProjectById(Guid id)
        {
            return await _mediator.Send(new GetProjectByIdQuery(id));
        }

        public async Task<List<ProjectDto>> GetProjectsByList()
        {
            return await _mediator.Send(new GetProjectsByListQuery());
        }

        public async Task<ProjectDto> GetProjectBySlug(string slug)
        {
            return await _mediator.Send(new GetProjectBySlugQuery(slug));
        }
    }
}
