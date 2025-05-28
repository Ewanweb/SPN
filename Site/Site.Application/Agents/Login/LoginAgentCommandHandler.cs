using MediatR;
using Microsoft.AspNetCore.Identity;
using Site.Application._shared;
using Site.Domain.Agents;

namespace Site.Application.Agents.Login;

public class LoginAgentCommandHandler : IRequestHandler<LoginAgentCommand, OperationResult>
{
    private readonly SignInManager<Agent> _signInManager;
    private readonly UserManager<Agent> _userManager;

    public LoginAgentCommandHandler(SignInManager<Agent> signInManager, UserManager<Agent> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }
    public async Task<OperationResult> Handle(LoginAgentCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Password) || string.IsNullOrWhiteSpace(request.Email))
            return OperationResult.Error("ایمیل و کلمه عبور اجباری هستند");

        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return OperationResult.Error("کاربری با این مشخصات یافت نشد");

        var result = await _signInManager.PasswordSignInAsync(user, request.Password, true, false);
        if (!result.Succeeded)
            return OperationResult.Error("ایمیل و کلمه عبور اجباری هستند");

        return OperationResult.Success("کاربر با موفقیت وارد شد");
    }
}