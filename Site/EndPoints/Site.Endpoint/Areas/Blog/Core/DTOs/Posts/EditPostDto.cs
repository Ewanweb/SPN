﻿using Microsoft.AspNetCore.Http;

namespace Site.Endpoint.Areas.Blog.Core.DTOs.Posts
{
    public class EditPostDto
    {
        public int PostId { get; set; }
        public int CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public bool IsSpecial { get; set; }

        public IFormFile ImageFile { get; set; }
    }
}