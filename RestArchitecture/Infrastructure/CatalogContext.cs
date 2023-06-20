using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class CatalogContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("CatalogDb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.HasMany<Item>()
                    .WithOne(c => c.Category)
                    .OnDelete(DeleteBehavior.ClientCascade);

                entity.Property(c => c.Name).IsRequired();
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(i => i.Id);

                entity.HasOne(i => i.Category)
                    .WithMany(c => c.Items)
                    .HasForeignKey(i => i.CategoryId);

                entity.Property(c => c.Name).IsRequired();
                entity.Property(c => c.Description).IsRequired();
            });
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}
