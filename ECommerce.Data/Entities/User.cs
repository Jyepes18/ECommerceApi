namespace ECommerce.Data.Entities;

public class User
{
    public int Id { get; set; }

    public string Names { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
    public bool IsCompany { get; set; }

    public int RoleId { get; set; }
    public string? Nit { get; set; }

    public string? NameCompany { get; set; }
}