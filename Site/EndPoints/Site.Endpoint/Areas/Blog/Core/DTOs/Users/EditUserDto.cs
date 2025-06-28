using Site.Endpoint.Areas.Blog.Data.Entities;

namespace Site.Endpoint.Areas.Blog.Core.DTOs.Users
{
    public class EditUserDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public UserRole Role { get; set; }

    }
}