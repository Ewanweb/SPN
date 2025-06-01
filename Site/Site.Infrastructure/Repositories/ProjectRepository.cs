using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Site.Domain.Projects;
using Site.Domain.Repositories;

namespace Site.Infrastructure.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        private readonly ApplicationDbContext _context;
        public ProjectRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Project?> GetProjectBySlug(string slug) =>
            await _context.Projects.FirstOrDefaultAsync(p => p.Slug == slug);

        public async Task<Project?> GetProjectWithImages(Guid projectId) =>
            await _context.Projects.Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == projectId);

    }
}
