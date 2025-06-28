using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Site.Domain.Agents;
using Site.Query.Agents.Dtos;

namespace Site.Query
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Agent, AgentDto>().ReverseMap();
        }
    }
}
