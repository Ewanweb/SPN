using AutoMapper;
using MediatR;
using Site.Domain.Repositories;
using Site.Query.Projects.Dtos;

namespace Site.Query.Projects.GetBySlug;

public class GetProjectBySlugQueryHandler : IRequestHandler<GetProjectBySlugQuery, ProjectDto>
{
    private readonly IProjectRepository _repository;
    private readonly IMapper _mapper;

    public GetProjectBySlugQueryHandler(IProjectRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ProjectDto> Handle(GetProjectBySlugQuery request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetProjectBySlug(request.Slug);

        if (project is null)
            throw new NullReferenceException("پروژه ای یافت نشد");

        return _mapper.Map<ProjectDto>(project);
    }
}