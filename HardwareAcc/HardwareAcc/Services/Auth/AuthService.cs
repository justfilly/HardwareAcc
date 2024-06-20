using System.Threading.Tasks;

namespace HardwareAcc.Services.AuthService;

public class AuthService : IAuthService
{
    public Task<bool> ValidateCredentialsAsync(string username, string password)
    {
        throw new System.NotImplementedException();
    }
}