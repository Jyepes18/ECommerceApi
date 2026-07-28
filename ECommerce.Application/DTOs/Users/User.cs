namespace ECommerce.Application.DTOs;

public class User
{
    public int Id { get; set; }

    public string Names { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public bool IsCompany { get; set; }

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string? Nit { get; set; }

    public string? NameCompany { get; set; }

    public int RoleId { get; set; }

}