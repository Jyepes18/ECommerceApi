using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs;

public class RegisterUserDto : IValidatableObject
{
    public required string Names { get; set; } 

    public required string LastName { get; set; }

    public required bool IsCompany { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }

    public string? Nit { get; set; }

    public string? NameCompany { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (IsCompany)
        {
            if (string.IsNullOrEmpty(Nit) && string.IsNullOrEmpty(NameCompany))
                yield return new ValidationResult("If is a company the nit and name company is obligatory");
        }
        
    }
}