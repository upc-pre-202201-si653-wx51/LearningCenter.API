using LearningCenter.API.Security.Authorization.Handlers.Interfaces;
using LearningCenter.API.Security.Authorization.Settings;
using LearningCenter.API.Security.Domain.Models;
using Microsoft.Extensions.Options;

namespace LearningCenter.API.Security.Authorization.Handlers.Implementations;

public class JwtHandler : IJwtHandler
{
    private readonly AppSettings _appSettings;

    public JwtHandler(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    public string GenerateToken(User user)
    {
        throw new NotImplementedException();
    }

    public int? ValidateToken(string token)
    {
        throw new NotImplementedException();
    }
}