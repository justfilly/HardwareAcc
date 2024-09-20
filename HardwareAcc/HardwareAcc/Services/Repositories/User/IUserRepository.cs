using System.Threading.Tasks;
using HardwareAcc.MVVM.Models;

namespace HardwareAcc.Services.Repositories.User;

public interface IUserRepository
{
    Task AddUserAsync(UserModel userModel);
    Task<UserModel?> GetUserByLoginAsync(string login);
    Task<UserModel?> GetUserByEmailAsync(string? email);
    Task<UserModel?> GetUserByPhoneNumberAsync(string? phoneNumber);
}