using System.Text.Json;
using CatAPI.Data;
using CatAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CatAPI.Services
{
    public class CatService
    {
        private readonly HttpClient _httpClient;

        private const string ApiKey = "YOUR_API_KEY";

        private readonly AppDbContext _context;

        public CatService(HttpClient httpClient,AppDbContext context)
        {
            _context = context;
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

        public async Task<CatImage> GetCatImageAsync(string id )
        {
            var response = await _httpClient.GetAsync($"images/{id}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<CatImage>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new CatImage();
        }

        public async Task SaveCatAsync(CatImage cat)
        {
            var exists = await _context.CatImages.AnyAsync(c => c.Id == cat.Id);
            if (!exists)
            {
                _context.CatImages.Add(cat);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<CatImage>> GetSavedCatImagesAsync() => await _context.CatImages.ToListAsync();

        public async Task<CatImage> DeleteCatAsync(string id)
        {
            var cat = await _context.CatImages.FindAsync(id);

            if (cat != null)
            {
                _context.CatImages.Remove(cat);
                await _context.SaveChangesAsync();
            }

            return cat;
        }
    }
}
