using Site.Endpoint.Areas.Blog.Core.DTOs.Categories;
using Site.Endpoint.Areas.Blog.Data.Entities;

namespace Site.Endpoint.Areas.Blog.Core.Mappers
{
    public class CategoryMapper
    {
        public static CategoryDto Map(Category category)
        {
            return new CategoryDto()
            {
                MetaDescription = category.MetaDescription,
                MetaTag = category.MetaTag,
                Slug = category.Slug,
                ParentId = category.ParentId,
                Id = category.Id,
                Title = category.Title
            };
        }
    }
}