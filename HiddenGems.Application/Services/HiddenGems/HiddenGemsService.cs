using HiddenGems.Application.Common.Repositories;
using HiddenGems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenGems.Application.Services.HiddenGems
{
    public class HiddenGemsService : IHiddenGemsService
    {
        private IUnitOfWork _unitOfWork;

        public HiddenGemsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddGem(string name, string description, string street, int number, string zip, string city, double? latitude, double? longitude, string imageUrl, int categoryId, string userId)
        {
            _unitOfWork.Gem.Add(name, description, street, number, zip, city, latitude, longitude, imageUrl, categoryId, userId);
            _unitOfWork.Save();
        }

        public void UpdateGem(Gem gem)
        {
            _unitOfWork.Gem.Update(gem);
            _unitOfWork.Save();
        }

        public void DeleteGem(int id)
        {
            _unitOfWork.Gem.Delete(id);
            _unitOfWork.Save();
        }

        public Gem GetGemById(int id)
        {
            return _unitOfWork.Gem.FindById(id);
        }

        public IEnumerable<Gem> GetAllGems()
        {
            return _unitOfWork.Gem.GetAll();
        }

        public void AddRating(int gemId, string userId, int stars, string comment, DateTime createdAt)
        {
            _unitOfWork.Rating.AddRating(gemId, userId, stars, comment, createdAt);
            _unitOfWork.Save();
        }

        public void UpdateRating(Rating rating)
        {
            _unitOfWork.Rating.UpdateRating(rating);
            _unitOfWork.Save();
        }

        public void DeleteRating(int id)
        {
            _unitOfWork.Rating.DeleteRating(id);
            _unitOfWork.Save();
        }

        public Rating GetRatingById(int id)
        {
            return _unitOfWork.Rating.FindById(id);
        }

        public IEnumerable<Rating> GetAllRatings()
        {
            return _unitOfWork.Rating.GetAllRatings();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _unitOfWork.Category.GetAllCategories();
        }

        public void AddFavorite(string userId, int gemId)
        {
            _unitOfWork.Favorite.AddFavorite(userId, gemId);
            _unitOfWork.Save();
        }

        public void RemoveFavorite(string userId, int gemId)
        {
            _unitOfWork.Favorite.RemoveFavorite(userId, gemId);
            _unitOfWork.Save();
        }

        public IEnumerable<Favorite> GetAllFavorites()
        {
            return _unitOfWork.Favorite.GetAllFavorites();
        }
    }
}
