using ECommerce.Application.DTOs.Carts;
using ECommerce.Application.Interfaces;
using ECommerce.Data.Context;
using ECommerce.Data.Entities;
using ECommerce.Data.Repositories.Interfaces;

namespace ECommerce.Application.Services;

public class CartService : ICartService
{
    private readonly ICartRespository _cartRespository;
    private readonly ApplicationDbContext _context;
    private readonly IProductRepository _productRepository;

    public CartService(ICartRespository cartRespository, ApplicationDbContext context, IProductRepository productRepository)
    {
        _cartRespository = cartRespository;
        _context = context;
        _productRepository = productRepository;
    }
    
    
    public async Task<Result<string, int>> Insert(int userId, Cart cart)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        
        CartEntity cartEntity = new CartEntity()
        {
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        int cartId = await _cartRespository.InsertIntoCart(cartEntity);
        if (cartId == 0)
        {
            await transaction.RollbackAsync();
            return Result<string, int>.Failure("Error to insert product into cart", 400);
        }

        List<CartItemsEntity> cartItemsEntity = new();

        foreach (var item in cart.CartItemsList)
        {
            cartItemsEntity.Add(new CartItemsEntity
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                CartId = cartId
            });
        }
        
        int insertItems = await _cartRespository.InsertIntoCartItems(cartItemsEntity);
        if (insertItems <= 0)
        {
            await transaction.RollbackAsync();
            return Result<string, int>.Failure("Error to insert product into cart", 400);
        }
        
        foreach (var productId in cart.CartItemsList)
        {
            int quantityProduct = await _productRepository.GetStockProduct(productId.ProductId);

            if (quantityProduct < productId.Quantity)
            {
                await transaction.RollbackAsync();
                return Result<string, int>.Failure("We doesn't have stock in this products", 404);
            }

            int newQuantity =- productId.Quantity;

            int updateQuantity = await _productRepository.UpdateQuantityProduct(newQuantity, productId.ProductId);
            if (updateQuantity == 0)
            {
                await transaction.RollbackAsync();
                return Result<string, int>.Failure("We doesn't update stock the product", 404);
            }
            
        }
        
        return Result<string, int>.Success("Products insert success", 201);
    }
    
    public async Task<Result<(ICollection<CartUser>, int Total), int>> GetAllUserCartItems(int userId, int page,
        int pageSize, CartFilter cartFilter)
    {
        int totalCartProductsUser = await _cartRespository.GetTotalItemsCartUser(userId);

        var listItemsCartUser =
            await _cartRespository.GetAllItemsCartUser(userId, page, pageSize, cartFilter.Name, cartFilter.Decripcion);
        
        return Result<(ICollection<CartUser>, int Total), int>.Success((listItemsCartUser, totalCartProductsUser), 200);

    }

    public async Task<Result<string, int>> Delete(int userId, int itemId)
    {
        bool itemFromUser = await _cartRespository.CanUserDelete(userId, itemId);
        if (!itemFromUser) return Result<string, int>.Failure("Error to delete item", 500);

        int deleteItem = await _cartRespository.DeleteItem(itemId);

        return deleteItem > 0
            ? Result<string, int>.Success("item delete success", 200)
            : Result<string, int>.Failure("Error to delete item", 500);
    }
    
}