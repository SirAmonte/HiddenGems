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
    public class FavoriteRepository : IFavoriteRepository
    {
        private AppDbContext _context;

        public FavoriteRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddFavorite(string userId, int gemId)
        {
            Favorite favorite = new Favorite
            {
                UserId = userId,
                GemId = gemId
            };
            _context.Favorites.Add(favorite);
        }

        public void RemoveFavorite(string userId, int gemId)
        {
            var favorite = _context.Favorites
                .FirstOrDefault(f => f.UserId == userId && f.GemId == gemId);
            if (favorite != null)
            {
                _context.Favorites.Remove(favorite);
            }
        }

        public IEnumerable<Favorite> GetAllFavorites()
        {
            return _context.Favorites.ToList();
        }
    }
}
