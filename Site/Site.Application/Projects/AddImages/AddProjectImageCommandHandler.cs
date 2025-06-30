using MediatR;
using Site.Application._shared;
using Site.Application._shared.FileUtil.Interfaces;
using Site.Domain.Projects;
using Site.Domain.Repositories;

namespace Site.Application.Projects.AddImages;

public class AddProjectImageCommandHandler : IRequestHandler<AddProjectImageCommand, OperationResult>
{
    private readonly IProjectRepository _repository;
    private readonly IFileService _fileService;

    public AddProjectImageCommandHandler(IProjectRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(AddProjectImageCommand request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetProjectByIdWithImages(request.ProjectId);

        if (project == null)
            return OperationResult.Error("پروژه یافت نشد");

        var newImages = new List<ProjectImage>();

        try
        {
            foreach (var formFile in request.Images)
            {
                if (formFile.Length > 0)
                {
                    var imageName = await _fileService.SaveFileAndGenerateName(formFile, Directories.ProjectImages);

                    newImages.Add(new ProjectImage(imageName, request.ProjectId));
                }
            }

            project.AddImages(newImages);

            await _repository.SaveChangesAsync();

            return OperationResult.Success("عکس ها با موفقیت به پروژه اضافه شدند");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return OperationResult.Error("عملیات شکست خورد");
        }
    }
}