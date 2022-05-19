using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace LearningCenter.API.Learning.Persistence.Contexts;

public class AppDbContext : DbContext
{
    
    public DbSet<Category> Categories { get; set; }
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Category>().ToTable("Categories");
        builder.Entity<Category>().HasKey(p => p.Id);
        builder.Entity<Category>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Category>().Property(p => p.Name).IsRequired().HasMaxLength(30);
        
        // Apply Snake Case Naming Convention
        
        builder.UseSnakeCaseNamingConvention();
    }
}