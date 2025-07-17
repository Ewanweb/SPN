using AutoMapper;
using MediatR;
using Site.Domain.Agents;
using Site.Domain.Repositories;
using Site.Query.Agents.Dtos;

namespace Site.Query.Agents.GetTeamAgents;

public class GetTeamAgentsQueryHandler : IRequestHandler<GetTeamAgentsQuery, List<AgentDto>>
{
    private readonly IAgentRepository _repository;
    private readonly IMapper _mapper;

    public GetTeamAgentsQueryHandler(IAgentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<List<AgentDto>> Handle(GetTeamAgentsQuery request, CancellationToken cancellationToken)
    {
        List<Agent>? agents = await _repository.GetTeamAgents();

        return _mapper.Map<List<AgentDto>>(agents);
    }
}