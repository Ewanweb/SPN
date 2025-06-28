using System.Collections.Generic;
using Site.Endpoint.Areas.Blog.Core.Utilities;

namespace Site.Endpoint.Areas.Blog.Core.DTOs.Posts
{
    public class PostFilterDto:BasePagination
    {
        public List<PostDto> Posts { get; set; }
        public PostFilterParams FilterParams { get; set; }
    }

    public class PostFilterParams
    {
        public string Title { get; set; }
        public string CategorySlug { get; set; }
        public int PageId { get; set; }
        public int Take { get; set; }
    }
}