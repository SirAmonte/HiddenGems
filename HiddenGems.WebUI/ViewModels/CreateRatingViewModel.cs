using System.ComponentModel.DataAnnotations;
using System.Drawing.Printing;

namespace HiddenGems.WebUI.ViewModels
{
    public class CreateRatingViewModel
    {
        public int Id { get; set; }

        [Required]
        public int GemId { get; set; }

        public string? UserId { get; set; }

        [Required]
        public int Stars { get; set; }

        [Required]
        public string Comment { get; set; }


    }
}
