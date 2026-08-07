using ECommerce.Data.Context;
using ECommerce.Data.Entities;
using ECommerce.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Data.Repositories;

public class CartRespository : ICartRespository
{
    private readonly ApplicationDbContext _context;

    public CartRespository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> InsertIntoCart(CartEntity cartEntity)
    {
        await _context.AddAsync(cartEntity);
        await _context.SaveChangesAsync();

        return cartEntity.Id;
    }
    
    public async Task<int> InsertIntoCartItems(List<CartItemsEntity> cartItemsEntity)
    {
        await _context.AddAsync(cartItemsEntity);
        return await _context.SaveChangesAsync();
    }


    public async Task<List<CartUser>> GetAllItemsCartUser(int userId, int page, int pageSize, string name, string description)
    {
        var query =
            from c in _context.cart
            join ci in _context.cart_items
                on c.Id equals ci.CartId
            join p in _context.product
                on ci.ProductId equals p.Id
            where c.UserId == userId
            select new CartUser
            {
                Id = c.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Quantity = ci.Quantity
            };

        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(x => x.Name.Contains(name));
        }

        if (!string.IsNullOrWhiteSpace(description))
        {
            query = query.Where(x => x.Description.Contains(description));
        }

        return await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
    
    public async Task<int> GetTotalItemsCartUser(int userId)
    {
        return await (
            from c in _context.cart
            join ci in _context.cart_items
                on c.Id equals ci.CartId
            where c.UserId == userId
            select ci.Quantity
        ).SumAsync();
    }
    
    public async Task<bool> CanUserDelete(int userId, int itemId)
    {
        bool belongsToUser = await _context.cart
            .AnyAsync(c => c.Id == itemId && c.UserId == userId);

        return belongsToUser;
    }
    
    public async Task<int> DeleteItem(int itemId)
    {
        return await _context.cart.Where(x => x.Id == itemId).ExecuteDeleteAsync();
    }
    
}