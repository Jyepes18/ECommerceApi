using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTOs.Products;

public class Product : IValidatableObject
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; } = 0;


    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Price <= 0)
            yield return new ValidationResult("Price can´t be 0 or minus");
    }
}