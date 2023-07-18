using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace FeatureFlag.Client
{
    public class ApiConfigurationSource : IConfigurationSource
    {
        public ApiConfigurationSource(string apiBaseUrl)
        {
            ApiBaseUrl = apiBaseUrl;
        }

        public string ApiBaseUrl { get; }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new ApiConfigurationProvider(ApiBaseUrl);
        }
    }

    public class ApiConfigurationProvider : ConfigurationProvider
    {
        public ApiConfigurationProvider(
            string apiBaseUrl)
        {
            ApiBaseUrl = apiBaseUrl;
        }

        public string ApiBaseUrl { get; }

        public override void Load()
        {
            try
            {
                using var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(ApiBaseUrl);
                var apiResponse = httpClient.GetAsync("featureflags").Result;
                var apiFeatureFlags = (apiResponse.Content.ReadFromJsonAsync<Dictionary<string, string?>>()).Result;
                if (apiFeatureFlags != null) Data = apiFeatureFlags;
            }
            catch (Exception ex) { }
        }
    }

    public static class ConfigurationExtensions
    {
        public static IConfigurationBuilder AddApiConfiguration(
            this IConfigurationBuilder builder,
            string apiBaseUrl)
        {
            return builder.Add(new ApiConfigurationSource(apiBaseUrl));
        }
    }
}
