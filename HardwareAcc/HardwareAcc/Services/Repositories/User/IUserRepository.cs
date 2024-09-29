using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HardwareAcc.MVVM.Models;

namespace HardwareAcc.Services.Repositories.User;

public interface IUserRepository
{
    event Action Changed;
    
    Task<IEnumerable<UserModel>> GetAllAsync();
    Task<UserModel> GetByLoginAsync(string login);
    Task<UserModel> GetByEmailAsync(string email);
    Task<UserModel> GetByPhoneNumberAsync(string phoneNumber);
    
    Task AddAsync(UserModel model);
    Task DeleteAsync(int id);
    Task UpdateAsync(UserModel model);
}