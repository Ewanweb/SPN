using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Site.Application._shared;
using Site.Domain._shared;
using Site.Domain.Agents;
using Site.Domain.Agents.ValueObjects;
using Site.Domain.Repositories;

namespace Site.Application.Agents.Register;

public class RegisterAgentCommandHandler : IRequestHandler<RegisterAgentCommand, OperationResult>
{
    private readonly IAgentRepository _repository;
    private readonly IHttpContextAccessor _httpContext;

    public RegisterAgentCommandHandler(IAgentRepository repository, IHttpContextAccessor httpContext)
    {
        _repository = repository;
        _httpContext = httpContext;
    }
    public async Task<OperationResult> Handle(RegisterAgentCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.FullName) ||
            string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Password) ||
            string.IsNullOrWhiteSpace(request.PhoneNumber))
        {
            return OperationResult.Error("همه فیلدها الزامی هستند.");
        }

        // بررسی موجود بودن ایمیل
        var existingEmail = await _repository.GetAgentByEmail(request.Email);

        if (existingEmail is not null)
            return OperationResult.Error("این ایمیل قبلاً ثبت ‌نام شده است.");

        // تبدیل شماره به ValueObject
        AgentPhoneNumber phoneNumber;
        try
        {
            phoneNumber = new AgentPhoneNumber(request.PhoneNumber);
        }
        catch (Exception ex)
        {
            return OperationResult.Error("شماره تماس نامعتبر است.");
        }

        // بررسی موجود بودن شماره تماس (بر اساس مقدار واقعی)
        var phoneExists = _repository.PhoneNumberExist(phoneNumber);

        if (phoneExists)
            return OperationResult.Error("شماره تماس قبلاً استفاده شده است.");

        try
        {
            // ساخت Agent
            var agent = Agent.Create(
                fullName: request.FullName,
                email: request.Email,
                password: request.Password,
                phoneNumber: phoneNumber
            );

            await _repository.AddAsync(agent);
            await _repository.SaveChangesAsync();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, agent.Id.ToString()),
                new Claim(ClaimTypes.Name, agent.FullName),
                new Claim(ClaimTypes.Email, agent.Email),
                new Claim("Slug", agent.Slug),
                new Claim("phone_number", agent.PhoneNumber.Value),
                new Claim("Status", agent.Status.ToString()),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            var authProps = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30)
            };

            await _httpContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProps);
            
            return OperationResult.Success("ثبت ‌نام با موفقیت انجام شد.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return OperationResult.Error($"{e}");
        }

    }
}