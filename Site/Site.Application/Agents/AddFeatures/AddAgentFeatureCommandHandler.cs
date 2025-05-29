using MediatR;
using Site.Application._shared;
using Site.Domain.Repositories;

namespace Site.Application.Agents.AddFeatures;

public class AddAgentFeatureCommandHandler : IRequestHandler<AddAgentFeatureCommand, OperationResult>
{

    private readonly IAgentRepository _repository;
    public AddAgentFeatureCommandHandler(IAgentRepository repository)
    {
        _repository = repository;
    }
    public async Task<OperationResult> Handle(AddAgentFeatureCommand request, CancellationToken cancellationToken)
    {
        var agent = await _repository.GetAgentBySlug(request.Slug);

        if (agent == null)
            return OperationResult.NotFound("ایجنت مورد نظر یافت نشد");

        try
        {
            agent.AddFeature(request.Features);

            await _repository.AddAsync(agent);
            await _repository.SaveChangesAsync();
            return OperationResult.Success("ویژگی ها با موفقیت اضافه شدند");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return OperationResult.Error("عملیات شکست خورد");
        }
    }
}