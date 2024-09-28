using System.Threading.Tasks;
using HardwareAcc.MVVM.Models;
using HardwareAcc.Services.Repositories;
using HardwareAcc.Services.Repositories.User;

namespace HardwareAcc.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    private UserModel _authenticatedUser; 
        
    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> ValidateLoginCredentialsAsync(string login, string password)
    {
        UserModel user = await _userRepository.GetUserByLoginAsync(login);

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

    public async Task RegisterAsync(UserModel userModel)
    {
        bool isCredentialsValid = await ValidateRegisterCredentialsAsync(userModel.Login, userModel.Email, userModel.PhoneNumber);

        if (isCredentialsValid) 
            await _userRepository.AddUserAsync(userModel);
    }

    public async Task<bool> ValidateRegisterCredentialsAsync(string login, string email = "", string phoneNumber = "")
    {
        UserModel userWithSameLogin = await _userRepository.GetUserByLoginAsync(login);
    
        if (userWithSameLogin != null)
            return false;

        if (string.IsNullOrEmpty(email) == false)
        {
            UserModel userWithSameEmail = await _userRepository.GetUserByEmailAsync(email);
        
            if (userWithSameEmail != null)
                return false;
        }

        if (string.IsNullOrEmpty(phoneNumber) == false)
        {
            UserModel userWithSamePhoneNumber = await _userRepository.GetUserByPhoneNumberAsync(phoneNumber);
            
            if (userWithSamePhoneNumber != null)
                return false;
        }

        return true;
    }
}