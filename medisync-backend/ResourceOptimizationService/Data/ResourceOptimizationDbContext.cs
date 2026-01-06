using Microsoft.EntityFrameworkCore;
using ResourceOptimizationService.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ResourceOptimizationService.Data
{
    public class ResourceOptimizationDbContext : DbContext
    {

        public ResourceOptimizationDbContext(DbContextOptions<ResourceOptimizationDbContext> options)
            : base(options)
        {

        }

        public DbSet<OptimizationRule> OptimizationRules { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<OptimizationRule>(entity =>
            {
                entity.HasKey(r => r.RuleId);

                entity.Property(r => r.RuleName)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(r => r.RuleValue)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(r => r.IsActive)
                      .IsRequired();

                entity.HasIndex(r => r.RuleName)
                      .IsUnique();
            });
        }

    }
}
