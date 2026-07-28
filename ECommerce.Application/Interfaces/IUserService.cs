using ECommerce.Application.DTOs;
using User = ECommerce.Data.Entities.User;

namespace ECommerce.Application.Interfaces;

public interface IUserService
{
    Task<Result<string, int>> InsertUserAsync(RegisterUserDto userDto);
    Task<Result<string, int>> UpdateUserAsync(int userId, RegisterUserDto userDto);
    Task<Result<string, int>> DelteUserAsync(int userId);
    Task<Result<User, int>> GetUserAsync(int userId);
}