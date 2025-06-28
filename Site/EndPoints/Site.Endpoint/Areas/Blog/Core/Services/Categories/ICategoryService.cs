using System.Collections.Generic;
using Site.Endpoint.Areas.Blog.Core.DTOs.Categories;
using Site.Endpoint.Areas.Blog.Core.Utilities;

namespace Site.Endpoint.Areas.Blog.Core.Services.Categories
{
    public interface ICategoryService
    {
        OperationResult CreateCategory(CreateCategoryDto command);
        OperationResult EditCategory(EditCategoryDto command);
        List<CategoryDto> GetAllCategory();
        List<CategoryDto> GetChildCategories(int parentId);
        CategoryDto GetCategoryBy(int id);
        CategoryDto GetCategoryBy(string slug);
        bool IsSlugExist(string slug);
    }
}