using ECommerce.Data.Entities;

namespace ECommerce.Data.Repositories.Interfaces;

public interface ICartRespository
{
    Task<int> InsertIntoCart(CartEntity cartEntity);
    Task<int> InsertIntoCartItems(List<CartItemsEntity> cartItemsEntity);
    Task<List<CartUser>> GetAllItemsCartUser(int userId, int page, int pageSize, string name, string description);
    Task<int> GetTotalItemsCartUser(int userId);
    Task<bool> CanUserDelete(int userId, int itemId);
    Task<int> DeleteItem(int itemId);
}