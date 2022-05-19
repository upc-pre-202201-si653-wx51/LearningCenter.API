using LearningCenter.API.Learning.Domain.Repositories;
using LearningCenter.API.Learning.Persistence.Contexts;

namespace LearningCenter.API.Learning.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
}