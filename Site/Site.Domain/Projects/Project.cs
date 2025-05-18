using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Site.Domain._shared;
using Site.Domain.Agents;

namespace Site.Domain.Projects
{
    public class Project : BaseEntity
    {
        public string Title { get; private set; }
        public string Slug { get; private set; }
        public string Description { get; private set; }
        public string MainImageName { get; private set; }
        public List<ProjectImage> Images { get; private set; }
        public List<Agent> Agents { get; private set; }

        private Project() { }

        public Project(string title, string slug, string description, string mainImageName)
        {
            Guard(title, description, mainImageName);
            Title = title;
            Slug = slug;
            Description = description;
            MainImageName = mainImageName;
        }

        public void Edit(string title, string slug, string description, string mainImageName)
        {
            Guard(title, description, mainImageName);
            Title = title;
            Slug = slug;
            Description = description;
            MainImageName = mainImageName;
        }


        public void AddImages(List<ProjectImage>? images)
        {
            if (images is null || !images.Any())
                throw new ArgumentException("هیچ تصویری ارسال نشده است", nameof(images));

            var projectImages = images
                .Select(image => new ProjectImage(image.ImageName, Id))
                .ToList();

            projectImages.AddRange(projectImages);
        }


        public void Guard(string title, string description, string mainImageName)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("عنوان نمی‌تواند خالی باشد", nameof(title));

            if (string.IsNullOrWhiteSpace(mainImageName))
                throw new ArgumentException("نام تصویر نمی‌تواند خالی باشد", nameof(mainImageName));

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("توضیحات نمی‌تواند خالی باشد", nameof(description));

        }
    }
}
