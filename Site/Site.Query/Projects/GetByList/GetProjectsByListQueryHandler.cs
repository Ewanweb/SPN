using AutoMapper;
using MediatR;
using Site.Domain.Repositories;
using Site.Query.Projects.Dtos;

namespace Site.Query.Projects.GetByList;

public class GetProjectsByListQueryHandler : IRequestHandler<GetProjectsByListQuery, List<ProjectDto>>
{
    private readonly IProjectRepository _repository;
    private readonly IMapper _mapper;

    public GetProjectsByListQueryHandler(IProjectRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<ProjectDto>> Handle(GetProjectsByListQuery request, CancellationToken cancellationToken)
    {
        var projects = await _repository.GetProjectsWithImages();

        return _mapper.Map<List<ProjectDto>>(projects);
    }
}