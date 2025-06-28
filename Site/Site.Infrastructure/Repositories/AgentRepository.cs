using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Site.Domain.Agents;
using Site.Domain.Repositories;

namespace Site.Infrastructure.Repositories
{
    public class AgentRepository : Repository<Agent>, IAgentRepository
    {
        private readonly ApplicationDbContext _context;
        public AgentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Agent?> GetAgentBySlug(string slug)
        {
           return await _context.Agents.FirstOrDefaultAsync(s => s.Slug == slug);
        }

        public async Task<Agent?> GetAgentById(string id)
            => await _context.Agents.FirstOrDefaultAsync(s => s.Id == id);
    }
}
