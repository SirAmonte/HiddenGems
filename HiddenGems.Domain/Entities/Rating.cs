using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenGems.Domain.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public int GemId { get; set; }
        public string UserId { get; set; }
        public int Stars { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
