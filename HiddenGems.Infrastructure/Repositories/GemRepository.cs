using HiddenGems.Application.Common.Repositories;
using HiddenGems.Domain.Entities;
using HiddenGems.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenGems.Infrastructure.Repositories
{
    public class GemRepository : IGemRepository
    {
        private AppDbContext _context;
        public GemRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(string name, string description, string street, int number, string zip, string city, double? latitude, double? longitude, string imageUrl, int categoryId, string userId)
        {
            Gem gem = new Gem
            {
                Name = name,
                Description = description,
                Street = street,
                Number = number,
                Zip = zip,
                City = city,
                Latitude = latitude,
                Longitude = longitude,
                ImageUrl = imageUrl,
                CategoryId = categoryId,
                UserId = userId
            };
            _context.Gems.Add(gem);
        }

        public void Update(Gem gem)
        {
            _context.Gems.Update(gem);
        }

        public void Delete(int id)
        {
            Gem gem = _context.Gems.Find(id);
            if (gem != null)
            {
                _context.Gems.Remove(gem);
            }
        }

        public Gem FindById(int id)
        {
            return _context.Gems.Find(id);
        }

        public IEnumerable<Gem> GetAll()
        {
            return _context.Gems.ToList();
        }
    }
}
