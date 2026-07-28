namespace ECommerce.Application.Services;

public interface ILoginService
{
    Task<Result<string, int>> Login(string email, string password);
}