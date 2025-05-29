using MediatR;
using Microsoft.AspNetCore.Identity;
using Site.Application._shared;
using Site.Domain._shared;
using Site.Domain.Agents;
using Site.Domain.Agents.ValueObjects;

namespace Site.Application.Agents.Register;

public class RegisterAgentCommandHandler : IRequestHandler<RegisterAgentCommand, OperationResult>
{
    private readonly UserManager<Agent> _userManager;
    private readonly SignInManager<Agent> _signInManager;

    public RegisterAgentCommandHandler(UserManager<Agent> userManager, SignInManager<Agent> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
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
        var existingEmail = await _userManager.FindByEmailAsync(request.Email);

        if (existingEmail is not null)
            return OperationResult.Error("این ایمیل قبلاً ثبت‌نام شده است.");

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
        var phoneExists = _userManager.Users.Any(u => u.PhoneNumber.Value == phoneNumber.Value);

        if (phoneExists)
            return OperationResult.Error("شماره تماس قبلاً استفاده شده است.");

        string slug = SlugHelper.GenerateSlug(request.FullName);

        // ساخت Agent
        var agent = Agent.Register(request.FullName, request.Email, phoneNumber, slug);

        // ساخت یوزر با Identity
        var result = await _userManager.CreateAsync(agent, request.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(" | ", result.Errors.Select(e => e.Description));

            return OperationResult.Error(errors);
        }
        await _signInManager.SignInAsync(agent, isPersistent: true);

        return OperationResult.Success("ثبت ‌نام با موفقیت انجام شد.");
    }
}