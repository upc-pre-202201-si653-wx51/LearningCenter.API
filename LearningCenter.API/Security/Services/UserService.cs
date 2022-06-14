using AutoMapper;
using LearningCenter.API.Security.Authorization.Handlers.Interfaces;
using LearningCenter.API.Security.Domain.Models;
using LearningCenter.API.Security.Domain.Repositories;
using LearningCenter.API.Security.Domain.Services;
using LearningCenter.API.Security.Domain.Services.Communication;
using LearningCenter.API.Security.Exceptions;
using LearningCenter.API.Shared.Domain.Repositories;
using BCryptNet = BCrypt.Net.BCrypt;

namespace LearningCenter.API.Security.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    private readonly IJwtHandler _jwtHandler;
    private readonly IMapper _mapper;


    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IJwtHandler jwtHandler, IMapper mapper)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _jwtHandler = jwtHandler;
        _mapper = mapper;
    }

    public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
    {
        var user = await _userRepository.FindByUsernameAsync(request.Username);
        Console.WriteLine($"Request: {request.Username}, {request.Password}");
        Console.WriteLine($"User: {user.Id}, {user.FirstName}, {user.LastName}, {user.Username}, {user.PasswordHash}");
        
        // Validate
        if (user == null || !BCryptNet.Verify(request.Password, user.PasswordHash))
        {
            Console.WriteLine("Authentication Error");
            throw new AppException("Username of password is incorrect");
        }
        
        Console.WriteLine("Authentication successful. About to generate token");
        
        //Authentication successful
        var response = _mapper.Map<AuthenticateResponse>(user);
        Console.WriteLine($"Response: {response.Id}, {response.FirstName}, {response.LastName}, {response.Username}");
        response.Token = _jwtHandler.GenerateToken(user);
        Console.WriteLine($"Generated Token is {response.Token}");
        return response;
    }

    public Task<IEnumerable<User>> ListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task RegisterAsync(RegisterRequest request)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(int id, UpdateRequest request)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}