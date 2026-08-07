using ECommerce.Application.DTOs.Carts;
using ECommerce.Data.Entities;

namespace ECommerce.Application.Interfaces;

public interface ICartService
{
    Task<Result<string, int>> Insert(int userId, Cart cart);

    Task<Result<(ICollection<CartUser>, int Total), int>> GetAllUserCartItems(int userId, int page,
        int pageSize, CartFilter cartFilter);
}