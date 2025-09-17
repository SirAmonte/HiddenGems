using HiddenGems.Domain.Entities;
using HiddenGems.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenGems.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Gem>()
                .HasOne<Category>()
                .WithMany()
                .HasForeignKey(g => g.CategoryId);

            modelBuilder.Entity<Rating>()
                .HasOne<Gem>()
                .WithMany()
                .HasForeignKey(r => r.GemId);

            modelBuilder.Entity<Favorite>()
                .HasOne<Gem>()
                .WithMany()
                .HasForeignKey(f => f.GemId);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Nature" },
                new Category { Id = 2, Name = "Culture" },
                new Category { Id = 3, Name = "Food & Drink" },
                new Category { Id = 4, Name = "Metropolitan" },
                new Category { Id = 5, Name = "Hidden Cafés" },
                new Category { Id = 6, Name = "Beach" },
                new Category { Id = 7, Name = "Historical Sites" },
                new Category { Id = 8, Name = "Parks & Gardens" },
                new Category { Id = 9, Name = "Local Markets" },
                new Category { Id = 10, Name = "Unique Shops" }
            );
        }

        public DbSet<Gem> Gems { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

    }
}
