using FeatureFlag.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;

var hostBuilder = Host.CreateApplicationBuilder(args);

hostBuilder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
hostBuilder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

hostBuilder.Configuration.AddApiConfiguration("https://localhost:7130/");

hostBuilder.Services.AddFeatureManagement();
hostBuilder.Services.AddHostedService<FeatureFlagUpdater>();

var host = hostBuilder.Build();
host.Run();