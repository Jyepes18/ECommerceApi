using ECommerce.Data.Entities;

namespace ECommerce.Data.Repositories.Interfaces;

public interface IUserRepository
{
    Task<bool> ValidateIfExistEmail(string email);
    Task<int> AddAsync(User user);
    Task<string?> GetPassword(string email);
    Task<User?> GetUserForEmail(string email);
    Task<bool> ValidateIfUserGetSameEmail(string email);
    Task<int> UpdateAsync(int userId, User user);
    Task<int> DeleteAsync(int userId);
    Task<User> GetUserId(int id);
}