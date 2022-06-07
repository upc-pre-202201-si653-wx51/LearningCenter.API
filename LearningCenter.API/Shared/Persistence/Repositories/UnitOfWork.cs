using LearningCenter.API.Shared.Domain.Repositories;
using LearningCenter.API.Shared.Persistence.Contexts;

namespace LearningCenter.API.Shared.Persistence.Repositories;

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