using HiddenGems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HiddenGems.Application.Common.Repositories
{
    public interface IRatingRepository
    {
        void AddRating(int gemId, string userId, int stars, string comment, DateTime createdAt);
        void UpdateRating(Rating rating);
        void DeleteRating(int id);
        Rating FindById(int id);
        IEnumerable<Rating> GetAllRatings();
    }
}
