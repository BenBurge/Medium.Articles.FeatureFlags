using Microsoft.EntityFrameworkCore;
using RestApi.Entities;

namespace RestApi
{
    public class FeatureFlagDbContext : DbContext
    {
        public FeatureFlagDbContext(DbContextOptions<FeatureFlagDbContext> options) :
            base(options)
        { }

        public DbSet<FeatureFlag> FeatureFlags => Set<FeatureFlag>();
    }
}
