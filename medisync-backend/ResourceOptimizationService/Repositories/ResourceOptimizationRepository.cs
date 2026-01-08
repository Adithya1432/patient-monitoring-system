using Microsoft.EntityFrameworkCore;
using ResourceOptimizationService.Data;
using ResourceOptimizationService.Interfaces;
using ResourceOptimizationService.Models;

namespace ResourceOptimizationService.Repositories
{
    public class ResourceOptimizationRepository:IResourceOptimizationRepository
    {
        public readonly ResourceOptimizationDbContext _context;
        public ResourceOptimizationRepository(ResourceOptimizationDbContext context)
        {
            _context = context;
        }

        public async Task<List<OptimizationRule>> GetActiveRulesAsync()
        {
            return await _context.OptimizationRules
                .Where(r => r.IsActive)
                .ToListAsync();
        }
    }
}
