using Site.Domain.Agents;

namespace Site.Domain.Repositories;

public interface IAgentRepository : IRepository<Agent>
{
    Task<Agent?> GetAgentBySlug(string slug);
    Task<Agent?> GetAgentById(string id);
}