using ECommerce.Data.Context;
using ECommerce.Data.Entities;
using ECommerce.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Data.Repositories;

public class UserRepository : IUserRepository 
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ValidateIfExistEmail(string email)
    {
        return await _context.users.AnyAsync(x => x.Email == email);
    }
    
    public async Task<int> AddAsync(User user)
    {
        await _context.users.AddAsync(user);

        return await _context.SaveChangesAsync();
    }
    
    public async Task<string?> GetPassword(string email)
    {
        return await _context.users.Where(x => x.Email == email).Select(x => x.Password).FirstOrDefaultAsync();
    }

    public async Task<User?> GetUserForEmail(string email)
    {
        return await _context.users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<bool> ValidateIfUserGetSameEmail(string email)
    {
        return await _context.users.AnyAsync(x => x.Email != email);
    }

    public async Task<int> UpdateAsync(int userId, User user)
    {
        var userDb = await _context.users.FirstOrDefaultAsync(x => x.Id == userId);
        
        if (userDb is null)
            return 0;
        
        userDb.Names = user.Names;
        userDb.LastName = user.LastName;
        userDb.Email = user.Email;
        userDb.IsCompany = user.IsCompany;
        userDb.Password = user.Password ?? userDb.Password;
        userDb.Nit = user.Nit;
        userDb.NameCompany = user.NameCompany;
        userDb.RoleId = user.RoleId;

        return await _context.SaveChangesAsync();

    }

    public async Task<int> DeleteAsync(int userId)
    {
        return await _context.users.Where(x => x.Id == userId).ExecuteDeleteAsync();
    }

    public async Task<User> GetUserId(int id)
    {
        return await _context.users.FirstOrDefaultAsync(x => x.Id == id);
    }

}