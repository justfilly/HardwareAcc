using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HardwareAcc.MVVM.Models;

namespace HardwareAcc.Services.Repositories.Status;

public interface IStatusRepository
{
    event Action Changed;
    Task<IEnumerable<StatusModel>> GetAllAsync();
    Task AddAsync(StatusModel model);
    Task DeleteAsync(int statusId);
    Task UpdateAsync(StatusModel model);
    Task<StatusModel> GetByNameAsync(string name);
}