using HiddenGems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenGems.Application.Common.Repositories
{
    public interface IGemRepository
    {
        void Add(string name, string description, string street, int number, string zip, string city, double? latitude, double? longitude, string imageUrl, int categoryId, string userId);
        void Update(Gem gem);
        void Delete(int id);
        Gem FindById(int id);
        IEnumerable<Gem> GetAll();
    }
}
