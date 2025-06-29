using AutoMapper;
using MediatR;
using Site.Domain.Repositories;
using Site.Query.Projects.Dtos;

namespace Site.Query.Projects.GetById;

public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDto>
{
    private readonly IProjectRepository _repository;
    private readonly IMapper _mapper;

    public GetProjectByIdQueryHandler(IProjectRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ProjectDto> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetProjectByIdWithImages(request.ProjectId);

        if (project is null)
            throw new NullReferenceException("پروژه ای یافت نشد");

        return _mapper.Map<ProjectDto>(project);
    }
}