using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Site.Application._shared;
using Site.Domain._shared;
using Site.Domain.Agents;
using Site.Domain.Agents.Enums;
using Site.Domain.Repositories;

namespace Site.Application.Agents.Login;

public class LoginAgentCommandHandler : IRequestHandler<LoginAgentCommand, OperationResult>
{
    private readonly IAgentRepository _repository;
    private readonly IHttpContextAccessor _httpContext;

    public LoginAgentCommandHandler(IAgentRepository repository, IHttpContextAccessor httpContext)
    {
        _repository = repository;
        _httpContext = httpContext;
    }

    public async Task<OperationResult> Handle(LoginAgentCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Password) || string.IsNullOrWhiteSpace(request.Email))
            return OperationResult.Error("ایمیل و کلمه عبور اجباری هستند");

        var user = await _repository.GetAgentByEmail(request.Email);

        if (user is null)
            return OperationResult.Error("کاربری با این مشخصات یافت نشد");

        if (!PasswordHasher.Verify(request.Password, user.Password))
            return OperationResult.Error("کاربری با این مشخصات یافت نشد");

        // ساخت Claimها
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("Slug", user.Slug),
            new Claim("phone_number", user.PhoneNumber.Value),
            new Claim("Status", user.Status.ToString()),
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        var authProps = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30)
        };

        await _httpContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProps);
        return OperationResult.Success("کاربر با موفقیت وارد شد");
    }
}