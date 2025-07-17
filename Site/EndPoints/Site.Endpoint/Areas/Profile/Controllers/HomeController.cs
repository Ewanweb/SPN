using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Site.Application._shared;
using Site.Application.Agents.AddFeatures;
using Site.Application.Agents.Edit;
using Site.Domain.Agents.ValueObjects;
using Site.Endpoint.Areas.Profile.ViewModels;
using Site.Facade.Agents;
using Site.Query.Agents.Dtos;
using AgentFeatureDto = Site.Application.Agents.AddFeatures.AgentFeatureDto;

namespace Site.Endpoint.Areas.Profile.Controllers
{
    [Area("Profile")]
    public class HomeController : Controller
    {
        private readonly IAgentFacade _facade;

        public HomeController(IAgentFacade facade)
        {
            _facade = facade;
        }

        public async Task<IActionResult> Index()
        {
            var user = HttpContext.User;


            if (user.Identity != null && user.Identity.IsAuthenticated)
            {
                var agentSlug = user.FindFirst("Slug")?.Value;

                var agent = await _facade.GetAgentBySlug(agentSlug!);
                
                var model = new ProfileViewModel()
                {
                    Id = agent.Id,
                    FullName = agent.FullName,
                    Email = agent.Email,
                    PhoneNumber = agent.PhoneNumber.Value,
                    GithubLink = agent.GithubLink,
                    ResumeFileName = agent.ResumeFileName!,
                    Description = agent.Description,
                    ImageName = agent.ImageName,
                    Slug = agentSlug!,
                    Profienece = agent.Profienece,
                    Experience = agent.Experience,
                    MyProfienece = agent.MyProfienece,

                };

                return View(model);
            }

            return RedirectToAction("Register", "Auth");

        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(string slug, ProfileViewModel model)
        {
            AgentDto? agent = await _facade.GetAgentBySlug(slug);

            if (agent is null)
            {
                TempData["Error"] = "کاربری یافت نشد";
                return RedirectToAction(nameof(Index));
            }

            AgentPhoneNumber phone = new AgentPhoneNumber(model.PhoneNumber);

            EditAgentCommand command = new EditAgentCommand(model.Slug, model.FullName, model.GithubLink
                , model.ImageFile, model.Description, model.Email, phone, model.ResumeFile, model.Profienece, model.MyProfienece, model.Experience);
            
            OperationResult result = await _facade.Edit(command);

            if (result.Status == OperationResultStatus.Error)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Index));
            }
            TempData["Success"] = result.Message;
            return RedirectToAction("Index", "Home", "Profile");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFeature(string slug, ProfileViewModel model)
        {


            var agent = await _facade.GetAgentBySlug(slug);
            if (agent == null)
                return NotFound();

            // فیلتر کردن ویژگی‌های جدید فقط با عنوان و درصد
            var newFeatures = model.NewFeatures;


            AddAgentFeatureCommand command = new AddAgentFeatureCommand(agent.Id, newFeatures.Title, newFeatures.Percentage);

            OperationResult result = await _facade.AddFeatures(command);

            if (result.Status == OperationResultStatus.Error)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Index));
            }
            TempData["Success"] = result.Message;

            return RedirectToAction(nameof(Index));
        }
    }
}
