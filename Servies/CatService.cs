using System.Text.Json;
using CatAPI.Models;

namespace CatAPI.Services
{
    public class CatService
    {
        private readonly HttpClient _httpClient;

        private const string ApiKey = "YOUR_API_KEY";

        public CatService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.thecatapi.com/v1/");
            _httpClient.DefaultRequestHeaders.Add("x-api-key", ApiKey);
        }

        public async Task<List<CatImage>> GetCatImagesAsync(int limit = 10)
        {
            var response = await _httpClient.GetAsync($"images/search?limit={limit}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<CatImage>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<CatImage>();
        }
    }
}
