using MediatR;
using Site.Application._shared;
using Site.Application._shared.FileUtil.Interfaces;
using Site.Domain.Repositories;

namespace Site.Application.Projects.RemoveImage;

public class RemoveImageCommandHandler : IRequestHandler<RemoveImageCommand, OperationResult>
{
    private readonly IProjectRepository _repository;
    private readonly IFileService _fileService;

    public RemoveImageCommandHandler(IProjectRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }
    public async Task<OperationResult> Handle(RemoveImageCommand request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetByIdAsync(request.ProjectId);

        if (project is null)
            return OperationResult.NotFound("پروژه ای یافت نشد");

        if (request.ImageNames is null || !request.ImageNames.Any())
            return OperationResult.Error("عکسی انتخاب نشده");

        try
        {
            var imagesToRemove = project.Images
                .Where(img => request.ImageNames.Contains(img.ImageName))
                .ToList();

            project.RemoveImage(imagesToRemove);

            foreach (var image in imagesToRemove)
            {
                var imageName = image.ImageName;
                _fileService.DeleteFile(Directories.ProjectImages, imageName);
            }

            await _repository.SaveChangesAsync();

            return OperationResult.Success("عکس ها با موفقیت حذف شدند");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return OperationResult.Error("عملیات شکست خورد");
        }

    }
}