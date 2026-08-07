using ECommerce.Data.Entities;

namespace ECommerce.Data.Repositories.Interfaces;

public interface IProductRepository
{
    Task<int> Insert(ProductEntity productEntity);

    Task<(ICollection<ProductEntity>, int Total)> GetAllByUser(int userId, int page, int pageSize, string? name,
        string? description, decimal? price);

    Task<int> Update(int id, ProductEntity productEntity, int userId);
    Task<int> Delete(int id, int userId);

    Task<(ICollection<ProductEntity>, int Total)> GetAll(int userId, int page, int pageSize, string? name,
        string? description, decimal? price);

    Task<int> GetStockProduct(int productId);
    Task<int> UpdateQuantityProduct(int newQunatity, int productId);
}