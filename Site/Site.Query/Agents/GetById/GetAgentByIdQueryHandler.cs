using AutoMapper;
using MediatR;
using Site.Domain.Repositories;
using Site.Query.Agents.Dtos;

namespace Site.Query.Agents.GetById;

public class GetAgentByIdQueryHandler : IRequestHandler<GetAgentByIdQuery, AgentDto>
{
    private readonly IAgentRepository _repository;
    private readonly IMapper _mapper;

    public GetAgentByIdQueryHandler(IAgentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<AgentDto> Handle(GetAgentByIdQuery request, CancellationToken cancellationToken)
    {
        var agent = await _repository.GetAgentById(request.UserId);

        if (agent is null)
            throw new NullReferenceException("کاربری با این مشخصات یافت نشد");

        return  _mapper.Map<AgentDto>(agent);
    }
}