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
        var agent = await _repository.GetByIdAsync(request.AgentId);
        if (agent == null)
            return OperationResult.Error("Agent not found");
        try
        {


            agent.AddFeatureGroup(request.AgentId, request.Title, request.Percentage);
            await _repository.SaveChangesAsync();

            return OperationResult.Success("ویژگی با موفقیت اضافه شد");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return OperationResult.Error($"عملیات شکست خورد {e}");

        }
    }
}