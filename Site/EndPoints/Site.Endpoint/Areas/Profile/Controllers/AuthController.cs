using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Site.Application._shared;
using Site.Application.Agents.Login;
using Site.Application.Agents.Register;
using Site.Domain.Agents;
using Site.Endpoint.Areas.Admin.ViewModels;
using Site.Endpoint.Areas.Profile.ViewModels;
using Site.Facade.Agents;
using Site.Query.Agents.Dtos;

namespace Site.Endpoint.Areas.Profile.Controllers
{
    [Area("Profile")]
    
    public class AuthController : Controller
    {
        private readonly IAgentFacade _facade;

        public AuthController(IAgentFacade facade)
        {
            _facade = facade;
        }

        public IActionResult Register()
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            RegisterAgentCommand command =
                new RegisterAgentCommand(model.FullName, model.email, model.Password, model.PhoneNumber);

            OperationResult result = await _facade.Register(command);

            if (result.Status is not OperationResultStatus.Success)
            {
                TempData["Error"] = result.Message;
                return View(model);
            }
            TempData["Success"] = result.Message;
            return RedirectToAction("Index", "Home");

        }

        public IActionResult Login()
        {
            if (HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            LoginAgentCommand command =
                new LoginAgentCommand(model.email, model.Password);

            OperationResult result = await _facade.Login(command);

            if (result.Status is not OperationResultStatus.Success)
            {
                TempData["Error"] = result.Message;
                return View(model);
            }
            TempData["Success"] = result.Message;
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth"); // یا صفحه اصلی
        }
        

    }
}
