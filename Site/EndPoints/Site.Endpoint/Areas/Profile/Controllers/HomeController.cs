using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Site.Endpoint.Areas.Profile.ViewModels;

namespace Site.Endpoint.Areas.Profile.Controllers
{
    [Area("Profile")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var user = HttpContext.User;


            if (user.Identity != null && user.Identity.IsAuthenticated)
            {
                var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var fullName = user.FindFirst(ClaimTypes.Name)?.Value;
                var email = user.FindFirst(ClaimTypes.Email)?.Value;
                var phone = user.FindFirst("phone_number")?.Value;

                var model = new ProfileViewModel
                {
                    Id = Guid.Parse(userId),
                    FullName = fullName,
                    Email = email,
                    PhoneNumber = phone
                };

                return View(model);
            }

            return RedirectToAction("Register", "Auth");

        }
    }
}
