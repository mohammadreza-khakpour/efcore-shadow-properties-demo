using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ShadowPropertiesDemo
{
    public class DataBaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=.;Initial Catalog=ShadowPropsDemo;Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>().Property<DateTime>("LastUpdated");
            base.OnModelCreating(modelBuilder); 
        }
        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            var modifiedBlogEntries = ChangeTracker.Entries<Blog>()
                .Where(entry => entry.State == EntityState.Added || entry.State == EntityState.Modified);
            foreach (var item in modifiedBlogEntries)
            {
                item.Property("LastUpdated").CurrentValue = DateTime.UtcNow;
            }
            return base.SaveChanges();
        }
        public DbSet<Blog> Blogs { get; set; }

    }
}