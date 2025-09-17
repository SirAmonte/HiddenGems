using HiddenGems.Application.Services.HiddenGems;
using HiddenGems.Domain.Entities;
using HiddenGems.Infrastructure.Identity;
using HiddenGems.WebUI.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HiddenGems.WebUI.Controllers
{
    public class RatingController : Controller
    {
        private readonly IFileUploadService _fileUploadService;
        private IHiddenGemsService _hiddenGemsService;
        private readonly UserManager<ApplicationUser> _userManager;

        public RatingController(IFileUploadService fileUploadService, IHiddenGemsService hiddenGemsService, UserManager<ApplicationUser> userManager)
        {
            _fileUploadService = fileUploadService;
            _hiddenGemsService = hiddenGemsService;
            _userManager = userManager;
        }

        public IActionResult Create(int id)
        {
            var model = new CreateRatingViewModel { GemId = id };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int id, CreateRatingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.GetUserAsync(User).Result;

                _hiddenGemsService.AddRating(
                        model.GemId,
                        user.Id,
                        model.Stars,
                        model.Comment,
                        DateTime.UtcNow
                );

                return RedirectToAction("Details", "Gem", new { id = id });
            }
            return View(model);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
