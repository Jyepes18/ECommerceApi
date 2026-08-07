using ECommerce.Application.DTOs.Products;
using ECommerce.Data.Entities;

namespace ECommerce.Application.Interfaces;

public interface IProductService
{
    Task<Result<string, int>> Create(Product product,  int UserId);

    Task<Result<(ICollection<ProductEntity>, int Total), int>> GetAllByUser(int userId, int page, int pageSize,
        ProductFilter productFilter);

    Task<Result<string, int>> Update(int id, Product product, int userId);
    Task<Result<string, int>> Delete(int id, int userId);

    Task<Result<(ICollection<ProductEntity>, int Total), int>> GetAll(int userId, int page,
        int pageSize, ProductFilter productFilter);
}