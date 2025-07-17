//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using MediatR;
//using Site.Domain.Repositories;
//using Site.Query.Agents.Dtos;

//namespace Site.Query.Agents.GetAgentFeatures
//{
//    public record GetAgentFeaturesQuery(Guid AgentId) : IRequest<List<AgentFeatureDto>>;

//    public class GetAgentFeaturesQueryHandler : IRequestHandler<GetAgentFeaturesQuery, List<AgentFeatureDto>>
//    {
//        private readonly IAgentRepository _context;

//        public GetAgentFeaturesQueryHandler(IAgentRepository context)
//        {
//            _context = context;
//        }

//        public async Task<List<AgentFeatureDto>> Handle(GetAgentFeaturesQuery request, CancellationToken cancellationToken)
//        {
//            var features = await _context.AgentFeatures
//                .Where(f => f.AgentId == request.AgentId)
//                .Select(f => new AgentFeatureDto(f.Title, f.Percentage))
//                .ToListAsync(cancellationToken);

//            return features;
//        }
//    }

//}
