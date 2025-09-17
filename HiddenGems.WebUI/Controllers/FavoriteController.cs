using HiddenGems.Application.Services.HiddenGems;
using HiddenGems.Domain.Entities;
using HiddenGems.Infrastructure.Identity;
using HiddenGems.WebUI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HiddenGems.WebUI.Controllers
{
    [Authorize]
    public class FavoriteController : Controller
    {
        private readonly IHiddenGemsService _hiddenGemsService;
        private readonly UserManager<ApplicationUser> _userManager;

        public FavoriteController(IHiddenGemsService hiddenGemsService, UserManager<ApplicationUser> userManager)
        {
            _hiddenGemsService = hiddenGemsService;
            _userManager = userManager;
        }

        public IActionResult Index(int? categoryId)
        {
            var userId = _userManager.GetUserId(User);

            var categories = _hiddenGemsService.GetAllCategories();
            var ratings = _hiddenGemsService.GetAllRatings();

            var userFavorites = _hiddenGemsService.GetAllFavorites()
                .Where(f => f.UserId == userId)
                .Select(f => f.GemId);

            var gems = _hiddenGemsService.GetAllGems()
                .Where(g => userFavorites.Contains(g.Id));

            if (categoryId.HasValue)
            {
                gems = gems.Where(g => g.CategoryId == categoryId.Value);
            }

            var viewModel = new ListGemViewModel
            {
                Gems = gems.ToList(),
                Categories = categories.ToList(),
                SelectedCategoryId = categoryId,
                Ratings = ratings.ToList(),
                UserFavorites = userFavorites
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToFavorites(int gemId)
        {
            _hiddenGemsService.AddFavorite(_userManager.GetUserId(User), gemId);
            return RedirectToAction("Index", "Gem");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id) 
        {
            _hiddenGemsService.RemoveFavorite(_userManager.GetUserId(User), id);
            return RedirectToAction("Index", "Gem");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleFavorite(int gemId)
        {
            var userId = _userManager.GetUserId(User);
            var isFavorite = _hiddenGemsService.GetAllFavorites()
                .Any(f => f.UserId == userId && f.GemId == gemId);

            if (isFavorite)
                _hiddenGemsService.RemoveFavorite(userId, gemId);
            else
                _hiddenGemsService.AddFavorite(userId, gemId);

            return RedirectToAction("Index", "Gem");
        }
    }
}
