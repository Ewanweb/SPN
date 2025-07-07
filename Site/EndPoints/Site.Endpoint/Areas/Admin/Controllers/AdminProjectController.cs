using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Site.Application._shared;
using Site.Application.Agents.Create;
using Site.Application.Projects.Create;
using Site.Domain.Projects;
using Site.Endpoint.Areas.Admin.ViewModels;
using Site.Facade.Projects;
using Site.Infrastructure;

namespace Site.Endpoint.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminProjectController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProjectFacade _facade;

        public AdminProjectController(ApplicationDbContext context, IProjectFacade facade)
        {
            _context = context;
            _facade = facade;
        }

        // GET: Admin/AdminProject
        public async Task<IActionResult> Index()
        {
            return View(await _facade.GetProjectsByList());
        }

        // GET: Admin/AdminProject/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Admin/AdminProject/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminProject/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectViewModels models)
        {
            CreateProjectCommand command = new CreateProjectCommand(models.Title, models.Description, models.Image);

            OperationResult agent = await _facade.Create(command);

            if (agent.Status is not OperationResultStatus.Success)
            {
                TempData["Error"] = agent.Message;
                return RedirectToAction(nameof(Index));
            }
            TempData["Success"] = agent.Message;
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/AdminProject/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Admin/AdminProject/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Title,Slug,Description,MainImageName,Id,CreatedDate,UpdatedDate")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Admin/AdminProject/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Admin/AdminProject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(Guid id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
