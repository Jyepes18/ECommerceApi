using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs.Products;

public class ProductFilter : IValidatableObject
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Price != null && Price < 0) 
            yield return new ValidationResult("Price can´t be less that 0");
    }
}