using Site.Domain.Agents.Enums;
using Site.Query.Agents.Dtos;

namespace Site.Endpoint.Areas.Admin.ViewModels;

public class ChangeAgentStatusViewModel
{
    public string AgentSlug { get; set; }
    public AgentStatus AgentStatus { get; set; }
}