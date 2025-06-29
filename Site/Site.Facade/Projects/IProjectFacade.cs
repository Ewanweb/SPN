using Site.Application._shared;
using Site.Application.Projects.AddImages;
using Site.Application.Projects.Create;
using Site.Application.Projects.Edit;
using Site.Application.Projects.RemoveImage;
using Site.Application.Projects.RemoveProject;
using Site.Query.Projects.Dtos;

namespace Site.Facade.Projects;

public interface IProjectFacade
{
    Task<OperationResult> AddImages(AddProjectImageCommand command);
    Task<OperationResult> Create(CreateProjectCommand command);
    Task<OperationResult> Edit(EditProjectCommand command);
    Task<OperationResult> Remove(RemoveProjectCommand command);
    Task<OperationResult> RemoveImage(RemoveImageCommand command);



    Task<ProjectDto> GetProjectById(Guid id);
    Task<List<ProjectDto>> GetProjectsByList();
    Task<ProjectDto> GetProjectBySlug(string slug);
}