using AutoMapper;
using MediatR;
using Site.Domain.Repositories;
using Site.Query.Agents.Dtos;

namespace Site.Query.Agents.GetByList;

public class GetAgentsByListQueryHandler : IRequestHandler<GetAgentsByListQuery, List<AgentDto>>
{
    private readonly IAgentRepository _repository;
    private readonly IMapper _mapper;

    public GetAgentsByListQueryHandler(IAgentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<AgentDto>> Handle(GetAgentsByListQuery request, CancellationToken cancellationToken)
    {
        var agents = await _repository.GetAllAsync();

        return _mapper.Map<List<AgentDto>>(agents);
    }
}