using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Site.Facade.Agents;
using Site.Query.Agents.Dtos;

namespace Site.Endpoint.Areas.Admin.Controllers
{
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
        public ActionResult Create(IFormCollection collection)
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

        // GET: AdminAgentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminAgentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
    }
}
