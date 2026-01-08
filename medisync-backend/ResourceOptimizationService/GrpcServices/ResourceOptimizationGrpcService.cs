using Grpc.Core;
using ResourceOptimizationService.Interfaces;
using Shared.Protos.Optimization;

namespace ResourceOptimizationService.GrpcServices
{
    public class ResourceOptimizationGrpcService : OptimizationService.OptimizationServiceBase
    {
        private readonly IResourceOptimizationService _service;

        public ResourceOptimizationGrpcService(IResourceOptimizationService service)
        {
            _service = service;
        }

        public override async Task<OptimizationRulesResponse> GetActiveRules(
            EmptyRequest request,
            ServerCallContext context)
        {
            var rules = await _service.GetActiveRulesAsync();

            var response = new OptimizationRulesResponse();
            response.Rules.AddRange(
                rules.Select(r => new OptimizationRule
                {
                    RuleName = r.RuleName,
                    RuleValue = r.RuleValue
                })
            );

            return response;
        }
    }
}
