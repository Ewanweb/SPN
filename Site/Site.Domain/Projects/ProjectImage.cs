using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Site.Domain._shared;

namespace Site.Domain.Projects
{
    public class ProjectImage : BaseEntity
    {
        public string ImageName { get; private set; }
        public Guid ProjectId { get; private set; }

        private ProjectImage() { }

        public ProjectImage(string imageName, Guid projectId)
        {
            if (string.IsNullOrWhiteSpace(imageName))
                throw new ArgumentException("نام تصویر نمی‌تواند خالی باشد", nameof(imageName));

            ImageName = imageName;
            ProjectId = projectId;
        }


    }
}
