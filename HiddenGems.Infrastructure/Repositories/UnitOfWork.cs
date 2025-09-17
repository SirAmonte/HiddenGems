using HiddenGems.Application.Common.Repositories;
using HiddenGems.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenGems.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IGemRepository _gemRepository;
        private ICategoryRepository _categoryRepository;
        private IRatingRepository _ratingRepository;
        private IFavoriteRepository _favoriteRepository;

        public IGemRepository Gem => _gemRepository;
        public ICategoryRepository Category => _categoryRepository;
        public IRatingRepository Rating => _ratingRepository;
        public IFavoriteRepository Favorite => _favoriteRepository;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            _gemRepository = new GemRepository(_context);
            _categoryRepository = new CategoryRepository(_context);
            _ratingRepository = new RatingRepository(_context);
            _favoriteRepository = new FavoriteRepository(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
