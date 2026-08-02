using ECommerce.Application.DTOs.Products;
using ECommerce.Application.Interfaces;
using ECommerce.Data.Entities;
using ECommerce.Data.Repositories.Interfaces;

namespace ECommerce.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<Result<string, int>> Create(Product product, int UserId)
    {
        ProductEntity insertNewProduct = new ProductEntity
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Quantity = product.Quantity,
            UserId = UserId
        };

        int newProduct = await _productRepository.Insert(insertNewProduct);
        if (newProduct > 0) return Result<string, int>.Success("The product created successfully", 201);

        return Result<string, int>.Failure("The product dosen´t inserted", 404);
    }

    public async Task<Result<(ICollection<ProductEntity>, int Total), int>> GetAllByUser(int userId, int page,
        int pageSize, ProductFilter productFilter)
    {
        var products = await _productRepository.GetAllByUser(userId, page, pageSize, productFilter.Name,
            productFilter.Description, productFilter.Price);

        return Result<(ICollection<ProductEntity>, int Total), int>.Success((products.Item1, products.Total), 200);
    }

    public async Task<Result<string, int>> Update(int id, Product product, int userId)
    {
        ProductEntity updateNewProduct = new ProductEntity
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Quantity = product.Quantity
        };

        int updateProduct = await _productRepository.Update(id, updateNewProduct, userId);
        if (updateProduct == 0) return Result<string, int>.Failure("Error to update product", 404);

        return Result<string, int>.Success("Product updated success", 200);
    }

    public async Task<Result<string, int>> Delete(int id, int userId)
    {
        int deleteProduct = await _productRepository.Delete(id);

        if (deleteProduct > 0) return Result<string, int>.Success("Product deleted success", 200);

        return Result<string, int>.Failure("Error to deleted product", 404);
    }
}