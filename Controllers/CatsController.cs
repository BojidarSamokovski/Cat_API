using Microsoft.AspNetCore.Mvc;
using CatAPI.Models;
using CatAPI.Services;
using Microsoft.EntityFrameworkCore;
using CatAPI.Data;

namespace CatAPI.Controllers
{
    public class CatsController : Controller
    {
        private readonly CatService _catService;

        public CatsController(CatService catService, AppDbContext context)
        {
            _catService = catService;
        }

        public async Task<IActionResult> Index()
        {
            var cats = await _catService.GetCatImagesAsync(12);

            return View(cats);
        }


        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var catImage = await _catService.GetCatImageAsync(id);
            if (catImage == null) return NotFound();

            return View(catImage);
        }

        public async Task<IActionResult> SaveToDb(string id)
        {
            // Взимаме котето от API-то
            var cat = await _catService.GetCatImageAsync(id);

            await _catService.SaveCatAsync(cat);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Saved()
        {
            var savedCats = await _catService.GetSavedCatImagesAsync();
            return View(savedCats);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var savedCats = await _catService.DeleteCatAsync(id);
            return RedirectToAction("Saved");
        }
    }
}
