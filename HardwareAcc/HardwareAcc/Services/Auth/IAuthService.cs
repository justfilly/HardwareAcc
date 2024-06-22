using System.Threading.Tasks;
using HardwareAcc.Models;

namespace HardwareAcc.Services.Auth;

public interface IAuthService
{
    Task<bool> ValidateLoginCredentialsAsync(string login, string password);
    Task LogInAsync(string login, string password);
    void LogOut();
    
    Task RegisterAsync(User user);
    Task<bool> ValidateRegisterCredentialsAsync(string login, string? email = "", string? phoneNumber = "");

}