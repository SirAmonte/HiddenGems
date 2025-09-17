using HiddenGems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenGems.Application.Common.Repositories
{
    public interface IFavoriteRepository
    {
        void AddFavorite(string userId, int gemId);

        void RemoveFavorite(string userId, int gemId);

        IEnumerable<Favorite> GetAllFavorites();
    }
}
