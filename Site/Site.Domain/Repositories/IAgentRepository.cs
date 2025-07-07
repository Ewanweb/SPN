using Site.Domain.Agents;
using Site.Domain.Agents.Enums;
using Site.Domain.Agents.ValueObjects;

namespace Site.Domain.Repositories;

public interface IAgentRepository : IRepository<Agent>
{
    Task<Agent?> GetAgentBySlug(string slug);
    Task<Agent?> GetAgentById(Guid id);
    Task<Agent?> GetAgentByEmail(string email);
    bool PhoneNumberExist(AgentPhoneNumber phoneNumber);
    Task<List<Agent>?> GetAgentByStatus(AgentStatus status);
}