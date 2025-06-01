using Site.Domain.Projects;

namespace Site.Domain.Repositories;

public interface IProjectRepository : IRepository<Project>
{
    Task<Project?> GetProjectBySlug(string slug);
    Task<Project?> GetProjectWithImages(Guid projectId);
}