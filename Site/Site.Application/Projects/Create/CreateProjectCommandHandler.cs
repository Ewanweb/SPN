using MediatR;
using Site.Application._shared;
using Site.Application._shared.FileUtil.Interfaces;
using Site.Domain._shared;
using Site.Domain.Projects;
using Site.Domain.Repositories;

namespace Site.Application.Projects.Create;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, OperationResult>
{
    private readonly IProjectRepository _repository;
    private readonly IFileService _fileService;

    public CreateProjectCommandHandler(IProjectRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }
    public async Task<OperationResult> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            return OperationResult.Error("عنوان نمیتواند خالی باشد");

        if (string.IsNullOrWhiteSpace(request.Description))
            return OperationResult.Error("توضیحات نمیتواند خالی باشد");

        if (request.Image is null)
            return OperationResult.Error("عکس نمیتواند خالی باشد");

        string slug = SlugHelper.GenerateSlug(request.Title);

        var projectExist = await _repository.GetProjectBySlug(slug);

        if (projectExist is not null)
            return OperationResult.Error("پروژه ای قبلا با این عنولن ثبت شده");


        try
        {
            var imageName = await _fileService.SaveFileAndGenerateName(request.Image, Directories.ProjectImages);

            var project = new Project(request.Title, slug, request.Description, imageName);

            await _repository.AddAsync(project);
            await _repository.SaveChangesAsync();

            return OperationResult.Success("پروژه با موفقیت ایجاد شد");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return OperationResult.Error("عملیات شکست خورد");
        }
    }
}