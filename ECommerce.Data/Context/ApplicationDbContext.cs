using ECommerce.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Data.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> users { get; set; }
    public DbSet<ProductEntity> product { get; set; } 
    public DbSet<CartEntity> cart { get; set; }
    public DbSet<CartItemsEntity> cart_items { get; set; }
}