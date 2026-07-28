using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Data.Repositories.Interfaces;
using User = ECommerce.Data.Entities.User;

namespace ECommerce.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    /// <summary>
    /// Register a new user
    /// </summary>
    /// <param name="userDto">w</param>
    /// <returns></returns>
    public async Task<Result<string, int>> InsertUserAsync(RegisterUserDto userDto)
    {
        bool theEmailExist = await _userRepository.ValidateIfExistEmail(userDto.Email);
        if (theEmailExist) 
            return Result<string, int>.Failure($"This Email {userDto.Email} already was taken", 400);
        
        var user = new User
        {
            Names = userDto.Names,
            LastName = userDto.LastName,
            Email = userDto.Email,
            IsCompany = userDto.IsCompany,
            Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
            RoleId = userDto.IsCompany ? 1 : 2,
            Nit = userDto.Nit,
            NameCompany = userDto.NameCompany
        };
        
        int rowsAffected = await _userRepository.AddAsync(user);
        if (rowsAffected > 0)
            return Result<string, int>.Success("User created successfully.", 201);
        
        return Result<string, int>.Failure("User doesn't created successfully",500);
    }
    
    public async Task<Result<string, int>> UpdateUserAsync(int userId, RegisterUserDto userDto)
    {
        bool emailHasOtherPerson = await _userRepository.ValidateIfUserGetSameEmail(userDto.Email);
        if (!emailHasOtherPerson) 
            return Result<string, int>.Failure($"This Email {userDto.Email} already was taken", 400);
        
        var user = new User
        {
            Names = userDto.Names,
            LastName = userDto.LastName,
            Email = userDto.Email,
            IsCompany = userDto.IsCompany,
            Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
            RoleId = userDto.IsCompany ? 1 : 2,
            Nit = userDto.Nit,
            NameCompany = userDto.NameCompany
        };
        
        int rowsAffected = await _userRepository.UpdateAsync(userId, user);
        if (rowsAffected > 0)
            return Result<string, int>.Success("User updated successfully.", 201);
        
        return Result<string, int>.Failure("User doesn't updated successfully",500);
    }
    
    public async Task<Result<string, int>> DelteUserAsync(int userId)
    {
        var deleteUser = await _userRepository.DeleteAsync(userId);
        if (deleteUser > 0) return Result<string, int>.Success("Ohh, bye", 200);
        
        return Result<string, int>.Failure("Ohh, we can´t delete you", 500);
    }
    
    public async Task<Result<User, int>> GetUserAsync(int userId)
    {
        User user = await _userRepository.GetUserId(userId);
        if (user == null) 
            return Result<User, int>.Failure("it is strainge because we don´t found you", 404);
        
        return Result<User, int>.Success(user, 200);
    }
}