using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenGems.Application.Common.Repositories
{
    public interface IUnitOfWork
    {
        IGemRepository Gem { get; }
        ICategoryRepository Category { get; }
        IRatingRepository Rating { get; }
        IFavoriteRepository Favorite { get; }
        void Save();
    }
}
