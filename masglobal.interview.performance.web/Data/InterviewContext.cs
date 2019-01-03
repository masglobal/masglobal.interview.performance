using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using masglobal.interview.performance.web.Util;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace masglobal.interview.performance.web.Models
{
    public class InterviewContext : DbContext
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public InterviewContext(DbContextOptions<InterviewContext> options)
            : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlite("Data Source=interview.db");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductCategory>()
                .HasKey(t => new { t.CategoryId, t.ProductId });

            modelBuilder.Entity<ProductCategory>()
                .HasOne(p => p.Product)
                .WithMany(pc => pc.ProductCategories)
                .HasForeignKey(p => p.ProductId);

            modelBuilder.Entity<ProductCategory>()
                .HasOne(c => c.Category)
                .WithMany(cp => cp.CategoryProducts)
                .HasForeignKey(c => c.CategoryId);

            


        }


        

        

        

        
    }
}
