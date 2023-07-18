using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApi.Entities;

namespace RestApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FeatureFlagsController : ControllerBase
    {
        public FeatureFlagsController(
            ILogger<FeatureFlagsController> logger,
            IDbContextFactory<FeatureFlagDbContext> dbContextFactory)
        {
            Logger = logger;
            DbContextFactory = dbContextFactory;
        }

        public ILogger<FeatureFlagsController> Logger { get; }
        public IDbContextFactory<FeatureFlagDbContext> DbContextFactory { get; }

        [HttpGet]
        public ActionResult<Dictionary<string, string?>?> Get()
        {
            try
            {
                using var dbContext = DbContextFactory.CreateDbContext();
                var featureFlagList = dbContext.FeatureFlags.AsNoTracking().ToList();
                var featureFlagDictionary = featureFlagList.ToDictionary(flag => $"FeatureManagement:{flag.Name}", flag => flag.IsEnabled.ToString());

                return featureFlagDictionary;
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, "Unable to process request for feature flag data. " +
                    "See inner exception for details.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public ActionResult<FeatureFlag?> Post(FeatureFlag featureFlag)
        {
            try
            {
                using var dbContext = DbContextFactory.CreateDbContext();

                var existingFlag = dbContext.FeatureFlags
                    .FirstOrDefault(flag => flag.Id == featureFlag.Id || flag.Name == featureFlag.Name);
                if (existingFlag != null) return BadRequest("Feature flag with this id or name already exists.");

                dbContext.FeatureFlags.Add(featureFlag);
                dbContext.SaveChanges();

                return featureFlag;
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, "Unable to create a new feature flag. " +
                    "See inner exception for details.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
