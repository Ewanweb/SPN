using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Site.Application.Agents.AddFeatures;
using Site.Domain.Agents;
using Site.Domain.Projects;
using Site.Query.Agents.Dtos;
using Site.Query.Projects.Dtos;

namespace Site.Query
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Agent, AgentDto>()
                .ForMember(dest => dest.AgentFeatures, opt => opt.MapFrom(src => src.AgentFeatures));

            CreateMap<AgentFeature, AgentFeatureDto>();
            CreateMap<Project, ProjectDto>().ReverseMap();
        }
    }
}
