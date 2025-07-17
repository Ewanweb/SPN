using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Site.Application._shared;
using Site.Application.Agents.Change_Status;
using Site.Application.Agents.Create;
using Site.Application.Agents.Edit;
using Site.Domain.Agents.Enums;
using Site.Endpoint.Areas.Admin.ViewModels;
using Site.Facade.Agents;
using Site.Query.Agents.Dtos;

namespace Site.Endpoint.Areas.Admin.Controllers
{
    [Area("admin")]
    public class AdminAgentController : Controller
    {
        private readonly IAgentFacade _facade;
        public AdminAgentController(IAgentFacade facade)
        {
            _facade = facade;
        }

        [Area("admin")]
        // GET: AdminAgentController
        public async Task<ActionResult<List<AgentDto>>> Index()
        {
            var agents = await _facade.GetAgentsByList();

            ViewData["Admin-Title"] = "لیست کاربران";

            return View(agents);
        }

        // GET: AdminAgentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminAgentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminAgentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection collection, AgentViewModels models)
        {
            try
            {
                CreateAgentCommand command = new CreateAgentCommand(models.FullName, models.Email, models.Password, models.PhoneNumber);

                OperationResult agent = await _facade.Create(command);

                if (agent.Status is not OperationResultStatus.Success)
                {
                    TempData["Error"] = agent.Message;
                    return RedirectToAction(nameof(Index));
                }
                TempData["Success"] = agent.Message;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(models);
            }
        }

        // GET: AdminAgentController/Edit/5
        public async Task<IActionResult> Edit(string slug)
        {
            AgentDto? agent = await _facade.GetAgentBySlug(slug);

            if (agent is null)
            {
                TempData["Error"] = "کاربری یافت نشد";
                return RedirectToAction(nameof(Index));
            }

            EditAgentViewModel viewModel = new EditAgentViewModel()
            {
                Description = agent.Description,
                Email = agent.Email,
                FullName = agent.FullName,
                PhoneNumber = agent.PhoneNumber,
                // ResumeFileName = agent.ResumeFileName,
                Slug = agent.Slug
            };
            ViewData["Admin-Title"] = "ویرایش کاربر";
            return View(viewModel);
        }

        // POST: AdminAgentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string slug, EditAgentViewModel viewModel,IFormCollection collection)
        {
            try
            {
                EditAgentCommand command = new EditAgentCommand(slug, viewModel.FullName, viewModel.GithubLink, null, viewModel.Description,
                    viewModel.Email, viewModel.PhoneNumber, null, viewModel.Profienece, viewModel.MyProfienece, viewModel.Experience);

                OperationResult agent = await _facade.Edit(command);

                if (agent.Status is not OperationResultStatus.Success)
                {
                    TempData["Error"] = agent.Message;
                    return RedirectToAction(nameof(Index));
                }
                TempData["Success"] = agent.Message;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminAgentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminAgentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminAgentController/ChangeStatus/Mahan
        public async Task<ActionResult> ChangeStatus(string slug)
        {
            AgentDto agent = await _facade.GetAgentBySlug(slug);
            var viewModel = new ChangeAgentStatusViewModel()
            {
                
                AgentSlug = agent.Slug,
                AgentStatus = agent.Status
            };
            return View(viewModel);
        }



        // POST: AdminAgentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeStatus(string slug, AgentStatus status)
        {
            try
            {
                ChangeAgentStatusCommand command = new ChangeAgentStatusCommand(slug, status);

                OperationResult changeStatus = await _facade.ChangeStatus(command);

                if (changeStatus.Status is not OperationResultStatus.Success)
                {
                    TempData["Error"] = changeStatus.Message;
                    return RedirectToAction(nameof(Index));
                }
                TempData["Success"] = changeStatus.Message;
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }
        }
    }
}
