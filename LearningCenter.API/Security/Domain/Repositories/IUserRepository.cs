using LearningCenter.API.Security.Domain.Models;

namespace LearningCenter.API.Security.Domain.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> ListAsync();
    Task AddAsync(User user);
    Task<User> FindByIdAsync(int id);
    Task<User> FindByNameAsync(string username);
    bool ExistsByUsername(string username);
    User FindById(int id);
    void Update(User user);
    void Delete(User user);
}