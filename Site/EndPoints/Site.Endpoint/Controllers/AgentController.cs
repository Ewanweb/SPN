using Microsoft.AspNetCore.Mvc;
using Site.Domain.Agents.Enums;
using Site.Facade.Agents;
using Site.Query.Agents.Dtos;

namespace Site.Endpoint.Controllers
{
    public class AgentController : Controller
    {
        private readonly IAgentFacade _facade;

        public AgentController(IAgentFacade facade)
        {
            _facade = facade;
        }

        public async Task<IActionResult> Index()
        {
            List<AgentDto> agents = await _facade.GetAgentsByStatus(AgentStatus.Agent);
            return View(agents);
        }
    }
}
