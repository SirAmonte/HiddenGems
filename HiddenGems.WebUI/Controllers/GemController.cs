using HiddenGems.Application.Services.HiddenGems;
using HiddenGems.Domain.Entities;
using HiddenGems.Infrastructure.Identity;
using HiddenGems.WebUI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HiddenGems.WebUI.Controllers
{
    [Authorize]
    public class GemController : Controller
    {
        private readonly IFileUploadService _fileUploadService;
        private IHiddenGemsService _hiddenGemsService;
        private readonly UserManager<ApplicationUser> _userManager;

        public GemController(IFileUploadService fileUploadService, IHiddenGemsService hiddenGemsService, UserManager<ApplicationUser> userManager)
        {
            _fileUploadService = fileUploadService;
            _hiddenGemsService = hiddenGemsService;
            _userManager = userManager;
        }

        public IActionResult Create()
        {
            CreateGemViewModel model = new CreateGemViewModel
            {
                Categories = _hiddenGemsService.GetAllCategories()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGemViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Image != null && model.Image.Length > 0)
                {
                    using (var stream = model.Image.OpenReadStream())
                    {
                        model.ImageURL = _fileUploadService.UploadFile(stream, model.Image.FileName);
                    }
                }
                var user = await _userManager.GetUserAsync(User);

                if (!model.Latitude.HasValue || !model.Longitude.HasValue)
                {
                    var coords = await GeocodeAddressAsync(model.Street, model.Number, model.Zip, model.City);
                    if (coords != null)
                    {
                        model.Latitude = coords.Value.lat;
                        model.Longitude = coords.Value.lon;
                    }
                }

                _hiddenGemsService.AddGem(
                    model.Name,
                    model.Description,
                    model.Street,
                    model.Number,
                    model.Zip,
                    model.City,
                    model.Latitude,
                    model.Longitude,
                    model.ImageURL,
                    model.SelectedCategoryId,
                    user.Id
                );

                return RedirectToAction("Index");
            }

            model.Categories = _hiddenGemsService.GetAllCategories();
            return View(model);
        }

        [Authorize(Policy = "AdminOnly")]
        public IActionResult Edit(int id)
        {
            var gem = _hiddenGemsService.GetGemById(id);
            if (gem == null)
            {
                return NotFound();
            }

            UpdateGemViewModel model = new UpdateGemViewModel()
            {
                Id = gem.Id,
                Name = gem.Name,
                Description = gem.Description,
                Street = gem.Street,
                Number = gem.Number,
                Zip = gem.Zip,
                City = gem.City,
                Latitude = gem.Latitude,
                Longitude = gem.Longitude,
                ImageURL = gem.ImageUrl,
                SelectedCategoryId = gem.CategoryId,
                Categories = _hiddenGemsService.GetAllCategories()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult Edit(UpdateGemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var gem = _hiddenGemsService.GetGemById(model.Id);
                if (gem == null)
                {
                    return NotFound();
                }

                if (model.Image != null && model.Image.Length > 0)
                {
                    using (var stream = model.Image.OpenReadStream())
                    {
                        model.ImageURL = _fileUploadService.UploadFile(stream, model.Image.FileName);
                    }
                }
                else
                {
                    model.ImageURL = gem.ImageUrl;
                }

                if (!model.Latitude.HasValue || !model.Longitude.HasValue)
                {
                    var coords = GeocodeAddressAsync(model.Street, model.Number, model.Zip, model.City).Result;
                    if (coords != null)
                    {
                        model.Latitude = coords.Value.lat;
                        model.Longitude = coords.Value.lon;
                    }
                }

                gem.Name = model.Name;
                gem.Description = model.Description;
                gem.Street = model.Street;
                gem.Number = model.Number;
                gem.Zip = model.Zip;
                gem.City = model.City;
                gem.Latitude = model.Latitude;
                gem.Longitude = model.Longitude;
                gem.ImageUrl = model.ImageURL;
                gem.CategoryId = model.SelectedCategoryId;

                _hiddenGemsService.UpdateGem(gem);

                return RedirectToAction("Index");
            }

            model.Categories = _hiddenGemsService.GetAllCategories();
            return View(model);
        }

        [Authorize(Policy = "AdminOnly")]
        public IActionResult Delete(int id)
        {
            _hiddenGemsService.DeleteGem(id);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id) 
        {
            var gem = _hiddenGemsService.GetGemById(id);
            var category = _hiddenGemsService.GetAllCategories().FirstOrDefault(c => c.Id == gem.CategoryId);
            var user = _userManager.Users.FirstOrDefault(u => u.Id == gem.UserId);
            var allRatings = _hiddenGemsService.GetAllRatings()
                .Where(r => r.GemId == gem.Id)
                .ToList();

            CreateGemViewModel model = new CreateGemViewModel
            {
                Id = gem.Id,
                Name = gem.Name,
                Description = gem.Description,
                Street = gem.Street,
                Number = gem.Number,
                Zip = gem.Zip,
                City = gem.City,
                Latitude = gem.Latitude,
                Longitude = gem.Longitude,
                ImageURL = gem.ImageUrl,
                SelectedCategoryId = gem.CategoryId,
                Categories = _hiddenGemsService.GetAllCategories(),
                CategoryName = category?.Name,
                UserName = $"{user?.FirstName} {user?.LastName}",
                Ratings = allRatings
            };

            return View(model);
        }

        public IActionResult Index(int? categoryId)
        {
            var categories = _hiddenGemsService.GetAllCategories();
            var gems = _hiddenGemsService.GetAllGems();
            var ratings = _hiddenGemsService.GetAllRatings();
            var userId = _userManager.GetUserId(User);

            if (categoryId.HasValue)
            {
                gems = gems.Where(g => g.CategoryId == categoryId.Value);
            }

            var userFavorites = _hiddenGemsService.GetAllFavorites()
                .Where(f => f.UserId == userId)
                .Select(f => f.GemId);

            var model = new ListGemViewModel
            {
                Gems = gems.ToList(),
                Categories = categories.ToList(),
                SelectedCategoryId = categoryId,
                Ratings = ratings.ToList(),
                UserFavorites = userFavorites
            };

            return View(model);
        }

        // Endpoint die coördinaten teruggeeft voor een gem ( ik gebruik het in de details view om een marker op de juiste locatie te zetten)
        [AllowAnonymous]
        public IActionResult Location(int id)
        {
            var gem = _hiddenGemsService.GetGemById(id);
            if (gem == null)
            {
                return NotFound();
            }
            return Json(new { latitude = gem.Latitude, longitude = gem.Longitude });
        }

        // Zet address om naar coördinaten (geocoding door gebruik te maken van Nominatim OpenStreetMap API)
        private async Task<(double lat, double lon)?> GeocodeAddressAsync(string street, int number, string zip, string city)
        {
            var address = $"{street} {number}, {zip} {city}";
            var url = $"https://nominatim.openstreetmap.org/search?format=json&q={Uri.EscapeDataString(address)}";
            using (var http = new HttpClient())
            {
                // User Agent is vereist door Nominatim
                http.DefaultRequestHeaders.UserAgent.ParseAdd("HiddenGemsApp/1.0");
                var response = await http.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var json = await response.Content.ReadAsStringAsync();
                var arr = System.Text.Json.JsonDocument.Parse(json).RootElement;
                if (arr.GetArrayLength() == 0) return null;

                // Neemt het eerste resultaat aangezien dat het meest pertinent is
                var first = arr[0];

                // probeert long en lat uit het resultaat te halen
                if (first.TryGetProperty("lat", out var latProp) && first.TryGetProperty("lon", out var lonProp))
                {
                    // dit converteert strings naar double
                    if (double.TryParse(latProp.GetString(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var lat) &&
                        double.TryParse(lonProp.GetString(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var lon))
                    {
                        return (lat, lon);
                    }
                }
                return null;
            }
        }
    }
}
