using Microsoft.AspNetCore.Components;
using System.Text;
using System.Text.Json;

namespace Chinook.ClientServices.API
{
    public class ApiServices : IApiServices
    {
        private readonly HttpClient HttpClient;
        private readonly ILogger<ApiServices> Logger;

        public ApiServices(HttpClient httpClient, NavigationManager navigationManager, ILogger<ApiServices> logger)
        {
            HttpClient = httpClient;
            HttpClient.BaseAddress = new Uri(navigationManager.BaseUri);
            Logger = logger;
        }

        /// <summary>
        /// Get data from server
        /// </summary>
        /// <typeparam name="T">The type of data to return</typeparam>
        /// <param name="url">The URL of the API endpoint</param>
        /// <returns>The deserialized data</returns>
        public async Task<T> GetDataAsync<T>(string url)
        {
            try
            {
                var response = await HttpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                Logger.LogInformation("Response Content: {0}", responseContent);

                var data = JsonSerializer.Deserialize<T>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return data ?? Activator.CreateInstance<T>();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Request error.");
                return Activator.CreateInstance<T>();
            }
        }

        public async Task<TResponse> PostDataAsync<TRequest, TResponse>(string url, TRequest requestData)
        {
            try
            {
                var requestContent = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
                var response = await HttpClient.PostAsync(url, requestContent);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                Logger.LogInformation("Response Content: {0}", responseContent);

                var responseData = JsonSerializer.Deserialize<TResponse>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return responseData;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Request error.");
                return default;
            }
        }
    }
}
