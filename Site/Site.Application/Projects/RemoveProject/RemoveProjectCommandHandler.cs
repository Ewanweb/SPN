using MediatR;
using Site.Application._shared;
using Site.Application._shared.FileUtil.Interfaces;
using Site.Domain.Repositories;

namespace Site.Application.Projects.RemoveProject;

public class RemoveProjectCommandHandler : IRequestHandler<RemoveProjectCommand, OperationResult>
{
    private readonly IProjectRepository _repository;
    private readonly IFileService _service;

    public RemoveProjectCommandHandler(IProjectRepository repository, IFileService service)
    {
        _repository = repository;
        _service = service;
    }
    public async Task<OperationResult> Handle(RemoveProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetByIdAsync(request.ProjectId);

        if (project is null)
            return OperationResult.NotFound("پروژه ای یافت نشد");

        _repository.Delete(project);

        await _repository.SaveChangesAsync();

        return OperationResult.Success("پروژه با موفقیت حذف شد");
    }
}