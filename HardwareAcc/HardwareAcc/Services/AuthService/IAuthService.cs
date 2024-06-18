using System.Threading.Tasks;

namespace HardwareAcc.Services.AuthService;

public interface IAuthService
{
    Task<bool> ValidateCredentialsAsync(string username, string password);
}