using System;

namespace Site.Endpoint.Areas.Blog.Core.DTOs.Comments
{
    public class CommentDto
    {
        public int CommentId { get; set; }
        public string UserFullName { get; set; }
        public int PostId { get; set; }
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }
    }
}