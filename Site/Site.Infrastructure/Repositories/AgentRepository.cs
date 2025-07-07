using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Site.Domain.Agents;
using Site.Domain.Agents.Enums;
using Site.Domain.Agents.ValueObjects;
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

        public async Task<Agent?> GetAgentById(Guid id)
            => await _context.Agents.FirstOrDefaultAsync(s => s.Id == id);

        public async Task<Agent?> GetAgentByEmail(string email)
            => await _context.Agents.FirstOrDefaultAsync(s => s.Email == email);
        public bool PhoneNumberExist(AgentPhoneNumber phoneNumber)
            =>  _context.Agents.Any(s => s.PhoneNumber.Value == phoneNumber.Value);

        public async Task<List<Agent>?> GetAgentByStatus(AgentStatus status)
            => await _context.Agents.Where(s => s.Status == status).ToListAsync();
    }
}
