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

    public async Task<IEnumerable<User>> ListAsync()
    {
        return await _userRepository.ListAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        var user = await _userRepository.FindByIdAsync(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }

    public async Task RegisterAsync(RegisterRequest request)
    {
        // Validate 
        if (_userRepository.ExistsByUsername(request.Username))
            throw new AppException($"Username '{request.Username}' is already taken");
        
        // Map Request to User Entity
        var user = _mapper.Map<User>(request);
        
        // Hash Password
        user.PasswordHash = BCryptNet.HashPassword(request.Password);
        
        // Save User

        try
        {
            await _userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while saving the user: {e.Message}");
        }
    }

    public async Task UpdateAsync(int id, UpdateRequest request)
    {
        var user = GetById(id);
        
        // Validate
        var userWithUsername = await _userRepository.FindByUsernameAsync(request.Username); 
        if ( userWithUsername != null && userWithUsername.Id != user.Id)
            throw new AppException($"Username '{request.Username}' is already taken");
        
        // Hash Password if it was entered
        if (!string.IsNullOrEmpty(request.Password))
            user.PasswordHash = BCryptNet.HashPassword(request.Password);
        
        // Map Request to User Entity
        _mapper.Map(request, user);
        try
        {
            _userRepository.Update(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while updating the user: {e.Message}");
        }

    }

    public async Task DeleteAsync(int id)
    {
        var user = GetById(id);
        try
        {
            _userRepository.Remove(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while deleting the user: {e.Message}");
        }
    }
    
    // Helper Methods

    private User GetById(int id)
    {
        var user = _userRepository.FindById(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }
}