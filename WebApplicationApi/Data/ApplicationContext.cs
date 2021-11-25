using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationApi.Models;

namespace WebApplicationApi.Data
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions options) :base(options)
        {

        }
      protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleCategory>().HasKey(sc => new { sc.ArticleId, sc.CategoryId });

            modelBuilder.Entity<ArticleCategory>()
                .HasOne<Article>(sc => sc.Article)
                .WithMany(s => s.ArticleCategory)
                .HasForeignKey(sc => sc.ArticleId);


            modelBuilder.Entity<ArticleCategory>()
                .HasOne<Category>(sc => sc.Category)
                .WithMany(s => s.ArticleCategory)
                .HasForeignKey(sc => sc.CategoryId);
        }
        public DbSet<Article> Article { get; set; }

        public DbSet<Category> Category { get; set; }

        public DbSet<ArticleCategory> ArticleCategory { get; set; }

        
    }
}
