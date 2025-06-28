using System.Collections.Generic;
using Site.Endpoint.Areas.Blog.Core.Utilities;

namespace Site.Endpoint.Areas.Blog.Core.DTOs.Users
{
    public class UserFilterDto:BasePagination
    {
        public List<UserDto> Users { get; set; }
    }

}