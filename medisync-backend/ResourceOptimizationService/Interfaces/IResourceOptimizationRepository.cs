using ResourceOptimizationService.Models;

namespace ResourceOptimizationService.Interfaces
{
    public interface IResourceOptimizationRepository
    {
        Task<List<OptimizationRule>> GetActiveRulesAsync();
    }
}
