using MediatR;
using Site.Application._shared;
using Site.Domain.Repositories;

namespace Site.Application.Agents.Edit;

public class EditAgentCommandHandler : IRequestHandler<EditAgentCommand, OperationResult>
{
    private readonly IAgentRepository _repository;
    public EditAgentCommandHandler(IAgentRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(EditAgentCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetAgentBySlug(request.Slug);

        if (user is null)
            return OperationResult.Error("کاربری با این مشخصات یافت نشد");

        try
        {
            user.Edit(request.FullName, request.GithubLink, request.ImageName,
                request.Description, request.Email, request.PhoneNumber);

            await _repository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return OperationResult.Error("عملیات شکست خورد");
        }

        return OperationResult.Success("عملیات موفقیت آمیز بود");

    }
}