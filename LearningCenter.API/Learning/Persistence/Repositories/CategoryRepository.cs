using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Repositories;
using LearningCenter.API.Learning.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LearningCenter.API.Learning.Persistence.Repositories;

public class CategoryRepository : BaseRepository, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Category>> ListAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task AddAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
    }

    public async Task<Category> FindByIdAsync(int id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public void Update(Category category)
    {
        _context.Categories.Update(category);
    }

    public void Remove(Category category)
    {
        _context.Categories.Remove(category);
    }
}