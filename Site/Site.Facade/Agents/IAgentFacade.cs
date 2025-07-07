using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Site.Application._shared;
using Site.Application.Agents.AddFeatures;
using Site.Application.Agents.Change_Status;
using Site.Application.Agents.Create;
using Site.Application.Agents.Edit;
using Site.Application.Agents.Login;
using Site.Application.Agents.Register;
using Site.Domain.Agents.Enums;
using Site.Query.Agents.Dtos;

namespace Site.Facade.Agents
{
    public interface IAgentFacade
    {
        Task<OperationResult> AddFeatures(AddAgentFeatureCommand command);
        Task<OperationResult> ChangeStatus(ChangeAgentStatusCommand command);
        Task<OperationResult> Edit(EditAgentCommand command);
        Task<OperationResult> Create(CreateAgentCommand command);
        Task<OperationResult> Register(RegisterAgentCommand command);
        Task<OperationResult> Login(LoginAgentCommand command);



        Task<AgentDto> GetAgentById(Guid id);
        Task<AgentDto> GetAgentBySlug(string slug);
        Task<List<AgentDto>> GetAgentsByList();
        Task<List<AgentDto>> GetAgentsByStatus(AgentStatus status);
    }
}
