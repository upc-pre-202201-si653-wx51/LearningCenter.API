using LearningCenter.API.Learning.Domain.Models;

namespace LearningCenter.API.Learning.Domain.Repositories;

public interface ITutorialRepository
{
    Task<IEnumerable<Tutorial>> ListAsync();
    Task AddAsync(Tutorial tutorial);
    Task<Tutorial> FindByIdAsync(int tutorialId);
    Task<Tutorial> FindByNameAsync(string name);
    Task<IEnumerable<Tutorial>> FindByCategoryIdAsync(int categoryId);
    void Update(Tutorial tutorial);
    void Remove(Tutorial tutorial);
}