using LearningCenter.API.Security.Domain.Models;

namespace LearningCenter.API.Security.Authorization.Handlers.Interfaces;

public interface IJwtHandler
{
    string GenerateToken(User user);
    int? ValidateToken(string token);
}