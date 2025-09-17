using HiddenGems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenGems.Application.Services.HiddenGems
{
    public interface IHiddenGemsService
    {
        void AddGem(string name, string description, string street, int number, string zip, string city, double? latitude, double? longitude, string imageUrl, int categoryId, string userId);
        void UpdateGem(Gem gem);
        void DeleteGem(int id);
        Gem GetGemById(int id);
        IEnumerable<Gem> GetAllGems();
        void AddRating(int gemId, string userId, int stars, string comment, DateTime createdAt);
        void UpdateRating(Rating rating);
        void DeleteRating(int id);
        Rating GetRatingById(int id);
        IEnumerable<Rating> GetAllRatings();
        IEnumerable<Category> GetAllCategories();
        void AddFavorite(string userId, int gemId);
        void RemoveFavorite(string userId, int gemId);
        IEnumerable<Favorite> GetAllFavorites();
    }
}
