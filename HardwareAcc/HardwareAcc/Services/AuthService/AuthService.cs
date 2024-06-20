using System;
using System.Diagnostics;
using System.Threading.Tasks;
using HardwareAcc.Models;
using HardwareAcc.Services.Repositories;

namespace HardwareAcc.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<bool> ValidateCredentialsAsync(string login, string password)
    {
        User? user = _userRepository.GetUserByLoginAsync(login).Result;
        
        throw new NotImplementedException();
    }
}