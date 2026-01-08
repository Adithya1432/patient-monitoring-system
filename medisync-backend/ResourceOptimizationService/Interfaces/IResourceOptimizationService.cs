using ResourceOptimizationService.Models;

namespace ResourceOptimizationService.Interfaces
{
    public interface IResourceOptimizationService
    {
        Task<List<OptimizationRule>> GetActiveRulesAsync();
    }
}
