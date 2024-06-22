using System.Threading.Tasks;
using HardwareAcc.Models;
using HardwareAcc.Services.Repositories;

namespace HardwareAcc.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> ValidateCredentialsAsync(string login, string password)
    {
        User? user = await _userRepository.GetUserByLoginAsync(login);

        if (user == null)
            return false;

        return user.Password == password;
    }
}