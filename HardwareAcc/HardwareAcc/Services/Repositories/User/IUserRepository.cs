using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HardwareAcc.MVVM.Models;

namespace HardwareAcc.Services.Repositories.User;

public interface IUserRepository
{
    event Action UsersChanged;
    
    Task<IEnumerable<UserModel>> GetAllUsersAsync();
    Task<UserModel> GetUserByLoginAsync(string login);
    Task<UserModel> GetUserByEmailAsync(string email);
    Task<UserModel> GetUserByPhoneNumberAsync(string phoneNumber);
    
    Task AddUserAsync(UserModel userModel);
    Task DeleteUserAsync(int id);
    Task UpdateUserAsync(UserModel userModel);
}