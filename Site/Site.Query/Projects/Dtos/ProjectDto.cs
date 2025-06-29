using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Site.Domain.Agents;
using Site.Domain.Projects;
using Site.Query.Agents.Dtos;

namespace Site.Query.Projects.Dtos
{
    public class ProjectDto : BaseDto
    {
        public string Title { get;  set; }
        public string Slug { get;  set; }
        public string Description { get;  set; }
        public string MainImageName { get;  set; }
        public List<ProjectImage> Images { get;  set; }
        public List<AgentDto> Agents { get;  set; }
    }
}
