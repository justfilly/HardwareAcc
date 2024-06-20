using System.Threading.Tasks;
using HardwareAcc.Models;

namespace HardwareAcc.Services.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByLoginAsync(string login);
}