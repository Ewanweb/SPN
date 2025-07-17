using AutoMapper;
using MediatR;
using Site.Domain.Agents;
using Site.Domain.Repositories;
using Site.Query.Agents.Dtos;

namespace Site.Query.Agents.GetTeamAgentBySlug;

public class GetTeamAgentBySlugQueryHandler : IRequestHandler<GetTeamAgentBySlugQuery, AgentDto>
{
    private readonly IAgentRepository _repository;
    private readonly IMapper _mapper;

    public GetTeamAgentBySlugQueryHandler(IAgentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<AgentDto> Handle(GetTeamAgentBySlugQuery request, CancellationToken cancellationToken)
    {
        if (request.Slug is null)
            throw new ArgumentNullException(nameof(request.Slug));

        Agent? agent = await _repository.GetTeamAgentBySlug(request.Slug);

        if (agent is null)
            throw new NullReferenceException(nameof(agent));


        return _mapper.Map<AgentDto>(agent);
    }
}