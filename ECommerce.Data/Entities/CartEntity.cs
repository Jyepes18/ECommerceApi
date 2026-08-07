namespace ECommerce.Data.Entities;

public class CartEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
}