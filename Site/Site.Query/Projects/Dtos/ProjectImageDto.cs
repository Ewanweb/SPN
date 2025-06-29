using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Site.Domain.Projects;

namespace Site.Query.Projects.Dtos
{
    public class ProjectImageDto : BaseDto
    {
        public string ImageName { get;  set; }
        public Guid ProjectId { get;  set; }
        public ProjectDto Project { get;  set; }
    }
}
