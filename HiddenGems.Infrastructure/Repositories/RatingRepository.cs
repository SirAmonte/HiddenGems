using HiddenGems.Application.Common.Repositories;
using HiddenGems.Domain.Entities;
using HiddenGems.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenGems.Infrastructure.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private AppDbContext _context;
        public RatingRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddRating(int gemId, string userId, int stars, string comment, DateTime createdAt)
        {
            Rating rating = new Rating
            {
                GemId = gemId,
                UserId = userId,
                Stars = stars,
                Comment = comment,
                CreatedAt = createdAt
            };
            _context.Ratings.Add(rating);
        }

        public void UpdateRating(Rating rating)
        {
            _context.Ratings.Update(rating);
        }

        public void DeleteRating(int id)
        {
            Rating rating = _context.Ratings.Find(id);
            if (rating != null)
            {
                _context.Ratings.Remove(rating);
            }
        }

        public Rating FindById(int id)
        {
            return _context.Ratings.Find(id);
        }

        public IEnumerable<Rating> GetAllRatings()
        {
            return _context.Ratings.ToList();
        }
    }
}
