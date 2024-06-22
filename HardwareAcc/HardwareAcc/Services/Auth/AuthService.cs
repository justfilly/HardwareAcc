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

    public async Task<bool> ValidateLoginCredentialsAsync(string login, string password)
    {
        User? user = await _userRepository.GetUserByLoginAsync(login);

        if (user == null)
            return false;

        return user.Password == password;
    }

    public async Task LogInAsync(string login, string password)
    {
        bool isCredentialsValid = await ValidateLoginCredentialsAsync(login, password);

        if (isCredentialsValid) 
            _authenticatedUser = await _userRepository.GetUserByLoginAsync(login);
    }

    public void LogOut() => 
        _authenticatedUser = null;

    public async Task RegisterAsync(User user)
    {
        bool isCredentialsValid = await ValidateRegisterCredentialsAsync(user.Login, user.Email, user.PhoneNumber);

        if (isCredentialsValid) 
            await _userRepository.AddUserAsync(user);
    }

    public async Task<bool> ValidateRegisterCredentialsAsync(string login, string? email = "", string? phoneNumber = "")
    {
        User? userWithSameLogin = await _userRepository.GetUserByLoginAsync(login);
    
        if (userWithSameLogin != null)
            return false;

        if (string.IsNullOrEmpty(email) == false)
        {
            User? userWithSameEmail = await _userRepository.GetUserByEmailAsync(email);
        
            if (userWithSameEmail != null)
                return false;
        }

        if (string.IsNullOrEmpty(phoneNumber) == false)
        {
            User? userWithSamePhoneNumber = await _userRepository.GetUserByPhoneNumberAsync(phoneNumber);
            
            if (userWithSamePhoneNumber != null)
                return false;
        }

        return true;
    }
}