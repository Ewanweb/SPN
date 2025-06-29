using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Site.Application._shared;
using Site.Application.Agents.AddFeatures;
using Site.Application.Agents.Change_Status;
using Site.Application.Agents.Create;
using Site.Application.Agents.Edit;
using Site.Application.Agents.Login;
using Site.Application.Agents.Register;
using Site.Query.Agents.Dtos;
using Site.Query.Agents.GetById;
using Site.Query.Agents.GetByList;
using Site.Query.Agents.GetBySlug;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Site.Facade.Agents
{
    public class AgentFacade : IAgentFacade
    {
        private readonly IMediator _mediator;

        public AgentFacade(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<OperationResult> AddFeatures(AddAgentFeatureCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> ChangeStatus(ChangeAgentStatusCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> Edit(EditAgentCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> Create(CreateAgentCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> Register(RegisterAgentCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<OperationResult> Login(LoginAgentCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<AgentDto> GetAgentById(string id)
        {
            return await _mediator.Send(new GetAgentByIdQuery(id));
        }

        public async Task<AgentDto> GetAgentBySlug(string slug)
        {
            return await _mediator.Send(new GetAgentBySlugQuery(slug));
        }

        public async Task<List<AgentDto>> GetAgentsByList()
        {
            return await _mediator.Send(new GetAgentsByListQuery());
        }
    }
}
