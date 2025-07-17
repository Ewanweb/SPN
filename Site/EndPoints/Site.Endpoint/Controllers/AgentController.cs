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
            List<AgentDto> agents = await _facade.GetTeamAgents();

            return View(agents);
        }
        
        public async Task<IActionResult> Single(string slug)
        {
            AgentDto agent = await _facade.GetTeamAgentBySlug(slug);
            
            return View(agent);
        }
    }
}
