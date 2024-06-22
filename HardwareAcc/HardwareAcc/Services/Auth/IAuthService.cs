using System.Threading.Tasks;

namespace HardwareAcc.Services.Auth;

public interface IAuthService
{
    Task<bool> ValidateCredentialsAsync(string login, string password);
}