using System.Threading.Tasks;
using HardwareAcc.Models;

namespace HardwareAcc.Services.Repositories;

public interface IUserRepository
{
    Task AddUserAsync(User user);
    Task<User?> GetUserByLoginAsync(string login);
    Task<User?> GetUserByEmailAsync(string? email);
    Task<User?> GetUserByPhoneNumberAsync(string? phoneNumber);
}