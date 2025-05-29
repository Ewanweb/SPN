using MediatR;
using Site.Application._shared;
using Site.Domain.Repositories;

namespace Site.Application.Agents.Change_Status;

public class ChangeAgentStatusCommandHandler : IRequestHandler<ChangeAgentStatusCommand, OperationResult>
{
    private readonly IAgentRepository _repository;

    public ChangeAgentStatusCommandHandler(IAgentRepository repository)
    {
        _repository = repository;
    }
    public async Task<OperationResult> Handle(ChangeAgentStatusCommand request, CancellationToken cancellationToken)
    {
        var agent = await _repository.GetAgentBySlug(request.Slug);

        if (agent is null)
            return OperationResult.Error("کاربری با این مشخصات یافت نشد");

        try
        {
            agent.ChangeStatus(request.Status);
            await _repository.SaveChangesAsync();
            return OperationResult.Success("وضعیت کاربر با موفقیت تغییر کرد");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return OperationResult.Error("عملیات شکست خورد");
        }
    }
}