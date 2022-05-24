using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Domain.Repositories;
using LearningCenter.API.Learning.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LearningCenter.API.Learning.Persistence.Repositories;

public class TutorialRepository : BaseRepository, ITutorialRepository
{
    public TutorialRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Tutorial>> ListAsync()
    {
        return await _context.Tutorials
            .Include(p => p.Category)
            .ToListAsync();
    }

    public async Task AddAsync(Tutorial tutorial)
    {
        await _context.Tutorials.AddAsync(tutorial);
    }

    public async Task<Tutorial> FindByIdAsync(int tutorialId)
    {
        return await _context.Tutorials
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == tutorialId);
        
    }

    public async Task<Tutorial> FindByTitleAsync(string title)
    {
        return await _context.Tutorials
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Title == title);
    }

    public async Task<IEnumerable<Tutorial>> FindByCategoryIdAsync(int categoryId)
    {
        return await _context.Tutorials
            .Where(p => p.CategoryId == categoryId)
            .Include(p => p.Category)
            .ToListAsync();
    }

    public void Update(Tutorial tutorial)
    {
        _context.Tutorials.Update(tutorial);
    }

    public void Remove(Tutorial tutorial)
    {
        _context.Tutorials.Remove(tutorial);
    }
}