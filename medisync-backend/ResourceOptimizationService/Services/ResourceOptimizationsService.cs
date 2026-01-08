using ResourceOptimizationService.Interfaces;
using ResourceOptimizationService.Models;

namespace ResourceOptimizationService.Services
{
    public class ResourceOptimizationsService: IResourceOptimizationService
    {
        public readonly IResourceOptimizationRepository _repository;
        public ResourceOptimizationsService(IResourceOptimizationRepository repository)
        {
            _repository = repository;
        }


        public Task<List<OptimizationRule>> GetActiveRulesAsync()
        {
            return _repository.GetActiveRulesAsync();
        }

    }
}
