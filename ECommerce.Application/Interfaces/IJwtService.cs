using ECommerce.Data.Entities;

namespace ECommerce.Application.Interfaces;

public interface IJwtService
{
    string GenerateEncodedTokenAsync(User user, string role);
}