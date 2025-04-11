using Microsoft.AspNetCore.Mvc;
using CatAPI.Models;
using CatAPI.Services;

namespace CatAPI.Controllers
{
    public class CatsController : Controller
    {
        private readonly CatService _catService;

        public CatsController(CatService catService)
        {
            _catService = catService;
        }

        public async Task<IActionResult> Index()
        {
            var cats = await _catService.GetCatImagesAsync(12);
            return View(cats);
        }
    }
}
