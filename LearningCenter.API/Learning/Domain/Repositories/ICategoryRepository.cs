using LearningCenter.API.Learning.Domain.Models;

namespace LearningCenter.API.Learning.Domain.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> ListAsync();
    Task AddAsync(Category category);
    Task<Category> FindByIdAsync(int id);
    void Update(Category category);
    void Remove(Category category);

}