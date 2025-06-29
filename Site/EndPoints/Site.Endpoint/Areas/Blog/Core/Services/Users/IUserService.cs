using Site.Endpoint.Areas.Blog.Core.DTOs.Users;
using Site.Endpoint.Areas.Blog.Core.Utilities;

namespace Site.Endpoint.Areas.Blog.Core.Services.Users
{
    public interface IUserService
    {
        OperationResult EditUser(EditUserDto command);
        OperationResult RegisterUser(UserRegisterDto registerDto);
        UserDto LoginUser(LoginUserDto loginDto);
        UserDto GetUserById(int userId);
        UserFilterDto GetUsersByFilter(int pageId, int take);
    }
}