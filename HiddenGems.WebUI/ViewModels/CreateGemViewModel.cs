using HiddenGems.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace HiddenGems.WebUI.ViewModels
{
    public class CreateGemViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        public string Zip { get; set; }
        [Required]
        public string City { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        [Display(Name = "Upload Image")]
        [Required]
        public IFormFile Image { get; set; }

        public string? ImageURL { get; set; }

        [Display(Name = "Category")]
        [Required]
        public int SelectedCategoryId { get; set; }

        public IEnumerable<Category>? Categories { get; set; }

        public string? CategoryName { get; set; }

        public string? UserName { get; set; }

        public IEnumerable<Rating>? Ratings { get; set; }

        public int RatingsCount { get; set; }

        public double AverageRating { get; set; }
    }
}
