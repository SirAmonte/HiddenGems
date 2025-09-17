using HiddenGems.Domain.Entities;

namespace HiddenGems.WebUI.ViewModels
{
    public class ListGemViewModel
    {
        public IEnumerable<Gem> Gems { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public int? SelectedCategoryId { get; set; }

        public IEnumerable<Rating> Ratings { get; set; }

        public int RatingsCount { get; set; }

        public double AverageRating { get; set; }
        public IEnumerable<int> UserFavorites { get; set; }
    }
}
