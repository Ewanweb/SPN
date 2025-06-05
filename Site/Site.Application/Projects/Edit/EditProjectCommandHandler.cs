using MediatR;
using Site.Application._shared;
using Site.Application._shared.FileUtil.Interfaces;
using Site.Domain._shared;
using Site.Domain.Repositories;

namespace Site.Application.Projects.Edit;

public class EditProjectCommandHandler : IRequestHandler<EditProjectCommand, OperationResult>
{
    private readonly IProjectRepository _repository;
    private readonly IFileService _fileService;

    public EditProjectCommandHandler(IProjectRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(EditProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetProjectBySlug(request.ProjectSlug);

        if (project is null)
            return OperationResult.Error("پروژه ای با این مشخصات پیدا نشد");


        try
        {

            string imageName;

            if (request.Image is null)
                imageName = project.MainImageName;
            else
                imageName = await _fileService.SaveFileAndGenerateName(request.Image, Directories.ProjectImages);

            var slug = SlugHelper.GenerateSlug(request.Title);

            project.Edit(request.Title, slug, request.Description, imageName);

            await _repository.SaveChangesAsync();
            return OperationResult.Success("پروژه با موفقیت ویرایش شد");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return OperationResult.Error("عملیات شکست خورد");
        }
    }
}