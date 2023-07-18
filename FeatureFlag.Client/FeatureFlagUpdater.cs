using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;

namespace FeatureFlag.Client
{
    public class FeatureFlagUpdater : BackgroundService
    {
        public FeatureFlagUpdater(
            ILogger<FeatureFlagUpdater> logger,
            IConfiguration configuration,
            IFeatureManager featureManager)
        {
            Logger = logger;
            Configuration = configuration;
            FeatureManager = featureManager;
        }

        public ILogger<FeatureFlagUpdater> Logger { get; }
        public IConfiguration Configuration { get; }
        public IFeatureManager FeatureManager { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    Logger.LogInformation("Attempting to reload configuration.");
                    (Configuration as IConfigurationRoot)?.Reload();
                    Logger.LogInformation("Feature Flag 1: {isEnabled}", await FeatureManager.IsEnabledAsync("Flag1"));
                    var allFeatures = FeatureManager.GetFeatureNamesAsync().ToBlockingEnumerable(cancellationToken: stoppingToken);

                    await Task.Delay(1000 * 30, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, "Unable to update feature flags due to an unexpected issue.");
            }
        }
    }
}
