using MediatR;
using Site.Application._shared;
using Site.Application._shared.FileUtil.Interfaces;
using Site.Domain.Repositories;

namespace Site.Application.Agents.Edit;

public class EditAgentCommandHandler : IRequestHandler<EditAgentCommand, OperationResult>
{
    private readonly IAgentRepository _repository;
    private readonly IFileService _fileService;
    public EditAgentCommandHandler(IAgentRepository repository, IFileService fileService)
    {
        _repository = repository;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(EditAgentCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetAgentBySlug(request.Slug);

        if (user is null)
            return OperationResult.Error("کاربری با این مشخصات یافت نشد");

        try
        {
            string image;
            string resume;

            if (request.Image is not null)
            {
                if (user.ImageName is not "noImage.png")
                    _fileService.DeleteFile(Directories.AgentImages, user.ImageName);
               
                image = await _fileService.SaveFileAndGenerateName(request.Image, Directories.AgentImages);
            }
            else
                image = user.ImageName;

            if (request.ResumeFile is not null)
            {
                if (user.ResumeFileName is not null)
                    _fileService.DeleteFile(Directories.AgentFiles, user.ResumeFileName!);

                resume = await _fileService.SaveFileAndGenerateName(request.ResumeFile, Directories.AgentFiles);
            }
            else
            {
                resume = null;
            }

            user.Edit(request.FullName, request.GithubLink, image,
                request.Description, request.Email, request.PhoneNumber, request.Profienece, request.Experience, request.MyProfienece, resume);

            await _repository.SaveChangesAsync();
            return OperationResult.Success("عملیات موفقیت آمیز بود");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return OperationResult.Error($"عملیات شکست خورد{e}");
        }

    }
}