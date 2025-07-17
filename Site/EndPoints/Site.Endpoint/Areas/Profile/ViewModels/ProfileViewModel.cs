using Site.Application.Agents.AddFeatures;

namespace Site.Endpoint.Areas.Profile.ViewModels
{
    public class ProfileViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Slug { get; set; }
        public string Email { get; set; }
        public string? Profienece { get; set; }
        public string? MyProfienece { get; set; }
        public string? Experience { get; set; }
        public string PhoneNumber { get; set; }
        public string? GithubLink { get;  set; }
        public string? Description { get;  set; }
        public string? ResumeFileName { get;  set; }
        public IFormFile? ResumeFile { get;  set; }
        public IFormFile? ImageFile { get;  set; }
        public string? ImageName { get;  set; }

        // ویژگی‌ها و گروه‌های تخصص
        public List<AgentFeatureDto> Features { get; set; } = new();
        public AgentFeatureDto NewFeatures { get; set; }

        // برای استفاده در مدال ویرایش}
    }
    
}
