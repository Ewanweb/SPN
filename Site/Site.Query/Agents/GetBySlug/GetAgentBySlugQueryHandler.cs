using AutoMapper;
using MediatR;
using Site.Domain.Repositories;
using Site.Query.Agents.Dtos;

namespace Site.Query.Agents.GetBySlug;

public class GetAgentBySlugQueryHandler : IRequestHandler<GetAgentBySlugQuery, AgentDto>
{
    private readonly IAgentRepository _repository;
    private readonly IMapper _mapper;

    public GetAgentBySlugQueryHandler(IAgentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<AgentDto> Handle(GetAgentBySlugQuery request, CancellationToken cancellationToken)
    {
        var agent = await _repository.GetAgentBySlug(request.Slug);

        if (agent is null)
            throw new NullReferenceException("کاربری یافت نشد");

        return _mapper.Map<AgentDto>(agent);
    }
}