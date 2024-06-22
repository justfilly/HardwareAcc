using System.Threading.Tasks;
using HardwareAcc.Models;
using HardwareAcc.Services.Repositories;

namespace HardwareAcc.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    private User? _authenticatedUser; 
        
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

    public async Task LogInAsync(string login, string password)
    {
        bool isCredentialsValid = await ValidateCredentialsAsync(login, password);

        if (isCredentialsValid) 
            _authenticatedUser = await _userRepository.GetUserByLoginAsync(login);
    }

    public void LogOut() => 
        _authenticatedUser = null;

    public async Task RegisterAsync(User user) => 
        await _userRepository.AddUserAsync(user);
}