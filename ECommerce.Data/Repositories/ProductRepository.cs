using ECommerce.Data.Context;
using ECommerce.Data.Entities;
using ECommerce.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Insert(ProductEntity productEntity)
    {
        await _context.product.AddAsync(productEntity);
        return await _context.SaveChangesAsync();
    }


    public async Task<(ICollection<ProductEntity>, int Total)> GetAllByUser(int userId, int page, int pageSize, string? name, 
        string? description, decimal? price)
    {
        var queryProducts = _context.product.AsQueryable();

        queryProducts = queryProducts.Where(x => x.UserId == userId);

        if (!string.IsNullOrWhiteSpace(name))
        {
            queryProducts = queryProducts.Where(x => x.Name == name);
        }

        if (!string.IsNullOrWhiteSpace(description))
        {
            queryProducts = queryProducts.Where(x => x.Description == description);
        }

        if (price.HasValue && price.Value > 0)
        {
            queryProducts = queryProducts.Where(x => x.Price == price.Value);
        }

        var totalProductByUser = await queryProducts.CountAsync();

        var products = await queryProducts
            .OrderBy(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (products, totalProductByUser);
    }
    
    
    public async Task<int> Update(int id, ProductEntity productEntity, int userId)
    {
        ProductEntity productdata = await _context.product.FirstOrDefaultAsync(x => x.Id == id);

        if (productdata.UserId != userId) return 0;

        productdata.Name = productEntity.Name;
        productdata.Description = productEntity.Description;
        productdata.Price = productEntity.Price;
        productdata.Quantity = productEntity.Quantity;

        return await _context.SaveChangesAsync();
    }

    public async Task<int> Delete(int id, int userId)
    {
        return await _context.product.
            Where(x => x.Id == id && x.UserId == userId).
            ExecuteDeleteAsync();
        
    }
    
    public async Task<(ICollection<ProductEntity>, int Total)> GetAll(int userId, int page, int pageSize, string? name, 
        string? description, decimal? price)
    {
        var queryProducts = _context.product.AsQueryable();

        if (!string.IsNullOrWhiteSpace(name)) queryProducts = queryProducts.Where(x => x.Name == name);

        if (!string.IsNullOrWhiteSpace(description))
            queryProducts = queryProducts.Where(x => x.Description == description);
        
        if (price.HasValue && price.Value > 0) queryProducts = queryProducts.Where(x => x.Price == price.Value);
        
        var totalProductByUser = await queryProducts.CountAsync();

        var products = await queryProducts
            .OrderBy(x => x.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (products, totalProductByUser);
    }

    public async Task<int> GetStockProduct(int productId)
    {
        return await _context.product.Where(x => x.Id == productId)
                .Select(x => x.Quantity)
                .FirstOrDefaultAsync();
    }


    public async Task<int> UpdateQuantityProduct(int newQunatity, int productId)
    {
        ProductEntity productdata = await _context.product.FirstOrDefaultAsync(x => x.Id == productId);
        if (productdata == null) return 0;

        productdata.Quantity = newQunatity;
        return await _context.SaveChangesAsync();
    }
}