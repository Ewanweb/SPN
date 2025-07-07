using AutoMapper;
using MediatR;
using Site.Domain.Agents;
using Site.Domain.Repositories;
using Site.Query.Agents.Dtos;

namespace Site.Query.Agents.GetByStatus;

public class GetAgentByStatusQueryHandler : IRequestHandler<GetAgentByStatusQuery, List<AgentDto>>
{
    private readonly IAgentRepository _repository;
    private readonly IMapper _mapper;

    public GetAgentByStatusQueryHandler(IAgentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<AgentDto>> Handle(GetAgentByStatusQuery request, CancellationToken cancellationToken)
    {
        List<Agent>? agents = await _repository.GetAgentByStatus(request.Status);

        return _mapper.Map<List<AgentDto>>(agents);
    }
}