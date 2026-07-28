using ECommerce.Application.Interfaces;
using ECommerce.Data.Entities;
using ECommerce.Data.Repositories.Interfaces;

namespace ECommerce.Application.Services;

public class LoginService : ILoginService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public LoginService(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }
    
    /// <summary>
    /// Login for a user, it is going to get the token
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<Result<string, int>> Login(string email, string password)
    {
        bool emailExist = await _userRepository.ValidateIfExistEmail(email);
        if (!emailExist) return Result<string, int>.Failure("Email o password as incorrect", 404);

        string? passwordHash = await _userRepository.GetPassword(email);
        if(string.IsNullOrEmpty(passwordHash)) return Result<string, int>.Failure("error to get password user", 500);

        bool verifyPasswords = BCrypt.Net.BCrypt.Verify(password, passwordHash);
        if(!verifyPasswords) return Result<string, int>.Failure("Email o password as incorrect", 404);

        User? user = await _userRepository.GetUserForEmail(email);
        if(user is null) return  Result<string, int>.Failure("data's user not found", 500);

        string token = _jwtService.GenerateEncodedTokenAsync(user, user.RoleId == 1 ? "ADMIN" : "USER");

        return Result<string, int>.Success(token, 200);
    }
}
