using System.Collections.Generic;
using CodeYad_Blog.CoreLayer.DTOs.Categories;
using Site.Endpoint.Areas.Blog.Core.DTOs.Posts;

namespace Site.Endpoint.Areas.Blog.Core.DTOs
{
    public class MainPageDto
    {
        public List<PostDto> LatestPosts { get; set; }
        public List<PostDto> SpecialPosts { get; set; }
        public List<MainPageCategoryDto> Categories { get; set; }
    }

    public class MainPageCategoryDto
    {
        public bool IsMainCategory { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public int PostChild { get; set; }
    }
}